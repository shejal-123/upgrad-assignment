using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

       //register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.RegisterAsync(dto);

            if (!result.Success)
                return Conflict(result); 

            return Created("", result); 
        }

        //login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result); 

            return Ok(result);
        }
    }
}