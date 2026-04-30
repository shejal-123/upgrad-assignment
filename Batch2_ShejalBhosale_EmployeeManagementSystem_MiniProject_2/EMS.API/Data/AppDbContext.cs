using Microsoft.EntityFrameworkCore;
using EMS.API.Models;

namespace EMS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var createdDate = new DateTime(2026, 4, 15);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Phone)
            .HasMaxLength(15)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FirstName = "Priya",
                LastName = "Sharma",
                Email = "priya@example.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Software Engineer",
                Salary = 50000,
                JoinDate = new DateTime(2022, 1, 10),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 2,
                FirstName = "Rahul",
                LastName = "Patil",
                Email = "rahul@example.com",
                Phone = "9123456780",
                Department = "HR",
                Designation = "HR Manager",
                Salary = 60000,
                JoinDate = new DateTime(2021, 6, 15),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 3,
                FirstName = "Sneha",
                LastName = "Joshi",
                Email = "sneha@example.com",
                Phone = "9988776655",
                Department = "Finance",
                Designation = "Accountant",
                Salary = 55000,
                JoinDate = new DateTime(2020, 3, 20),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 4,
                FirstName = "Amit",
                LastName = "Kulkarni",
                Email = "amit@example.com",
                Phone = "9012345678",
                Department = "Marketing",
                Designation = "Marketing Lead",
                Salary = 65000,
                JoinDate = new DateTime(2019, 11, 5),
                Status = "Inactive",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 5,
                FirstName = "Neha",
                LastName = "Desai",
                Email = "neha@example.com",
                Phone = "9090909090",
                Department = "Operations",
                Designation = "Operations Manager",
                Salary = 70000,
                JoinDate = new DateTime(2023, 2, 1),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 6,
                FirstName = "Rohit",
                LastName = "Mehta",
                Email = "rohit@example.com",
                Phone = "9876501234",
                Department = "Engineering",
                Designation = "Backend Developer",
                Salary = 52000,
                JoinDate = new DateTime(2021, 8, 12),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 7,
                FirstName = "Anjali",
                LastName = "Verma",
                Email = "anjali@example.com",
                Phone = "9988001122",
                Department = "Marketing",
                Designation = "SEO Specialist",
                Salary = 48000,
                JoinDate = new DateTime(2022, 4, 10),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 8,
                FirstName = "Karan",
                LastName = "Singh",
                Email = "karan@example.com",
                Phone = "9123409876",
                Department = "Finance",
                Designation = "Financial Analyst",
                Salary = 58000,
                JoinDate = new DateTime(2020, 9, 5),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 9,
                FirstName = "Pooja",
                LastName = "Nair",
                Email = "pooja@example.com",
                Phone = "9870012345",
                Department = "HR",
                Designation = "HR Executive",
                Salary = 45000,
                JoinDate = new DateTime(2023, 1, 15),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 10,
                FirstName = "Vikas",
                LastName = "Reddy",
                Email = "vikas@example.com",
                Phone = "9001122334",
                Department = "Operations",
                Designation = "Operations Executive",
                Salary = 47000,
                JoinDate = new DateTime(2021, 12, 20),
                Status = "Inactive",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 11,
                FirstName = "Meena",
                LastName = "Iyer",
                Email = "meena@example.com",
                Phone = "9098765432",
                Department = "Finance",
                Designation = "Senior Accountant",
                Salary = 62000,
                JoinDate = new DateTime(2019, 7, 25),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 12,
                FirstName = "Arjun",
                LastName = "Kapoor",
                Email = "arjun@example.com",
                Phone = "9812345670",
                Department = "Engineering",
                Designation = "Frontend Developer",
                Salary = 51000,
                JoinDate = new DateTime(2022, 6, 30),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 13,
                FirstName = "Divya",
                LastName = "Agarwal",
                Email = "divya@example.com",
                Phone = "9873216540",
                Department = "Marketing",
                Designation = "Content Strategist",
                Salary = 49000,
                JoinDate = new DateTime(2021, 3, 18),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 14,
                FirstName = "Suresh",
                LastName = "Yadav",
                Email = "suresh@example.com",
                Phone = "9988771122",
                Department = "Operations",
                Designation = "Logistics Manager",
                Salary = 63000,
                JoinDate = new DateTime(2020, 11, 11),
                Status = "Inactive",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            },
            new Employee
            {
                Id = 15,
                FirstName = "Kavita",
                LastName = "Shah",
                Email = "kavita@example.com",
                Phone = "9011223344",
                Department = "HR",
                Designation = "HR Manager",
                Salary = 64000,
                JoinDate = new DateTime(2018, 5, 9),
                Status = "Active",
                CreatedAt = createdDate,
                UpdatedAt = createdDate
            }
        );

        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "$2a$11$5WC2JgsDOnVqtc3ZpXMV..l9CBtsOt7wAh19hZnZP5lZXtJzFV6v2",
                Role = "Admin",
                CreatedAt = createdDate
            },
            new AppUser
            {
                Id = 2,
                Username = "viewer",
                PasswordHash = "$2a$11$hd7FYHd/M2N14su7R1MHHebMqCW2Q8zdUoKA1vEyqxA3dGYS4CSUG",
                Role = "Viewer",
                CreatedAt = createdDate
            }
        );
    }
}