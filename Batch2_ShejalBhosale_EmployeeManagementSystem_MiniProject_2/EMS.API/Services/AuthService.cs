using EMS.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EMS.API.Data;
using EMS.API.Models;

namespace EMS.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var exists = await _db.Users
                .AnyAsync(u => u.Username.ToLower() == dto.Username.ToLower());

            if (exists)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Username already exists"
                };
            }

        
            string role = "Viewer"; 

            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                var inputRole = dto.Role.Trim();

                if (inputRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    role = "Admin";
                else if (inputRole.Equals("Viewer", StringComparison.OrdinalIgnoreCase))
                    role = "Viewer";
                else
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Role must be Admin or Viewer"
                    };
                }
            }

            var user = new AppUser
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12),
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new AuthResponseDto
            {
                Success = true,
                Username = user.Username,
                Role = user.Role,
                Message = "Registered successfully"
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Username = user.Username,
                Role = user.Role,
                Token = token,
                Message = "Logged In successfully",

            };
        }

        private string GenerateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryHours = double.Parse(_config["Jwt:ExpiryHours"] ?? "8");

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}