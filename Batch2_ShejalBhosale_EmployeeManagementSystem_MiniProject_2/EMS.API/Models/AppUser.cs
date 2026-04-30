using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "Viewer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}