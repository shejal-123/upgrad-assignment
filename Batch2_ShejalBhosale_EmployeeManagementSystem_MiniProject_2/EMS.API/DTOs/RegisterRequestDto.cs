using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be 3–50 characters")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("Admin|Viewer", ErrorMessage = "Role must be Admin or Viewer")]
    public string? Role { get; set; } = "Viewer";
}