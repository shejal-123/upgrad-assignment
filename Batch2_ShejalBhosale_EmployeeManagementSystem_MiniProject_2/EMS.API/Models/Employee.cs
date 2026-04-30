using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public string Department { get; set; } = null!;

    [Required]
    public string Designation { get; set; } = null!;

    [Range(1, double.MaxValue)]
    public decimal Salary { get; set; }

    public DateTime JoinDate { get; set; }

    [Required]
    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}