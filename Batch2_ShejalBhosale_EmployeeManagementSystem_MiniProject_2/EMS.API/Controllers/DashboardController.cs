using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.API.Data;
using EMS.API.Models;

namespace EMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/employees/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDashboard()
        {
            //all
            var total = await _db.Employees.CountAsync();

            //active
            var active = await _db.Employees.CountAsync(e => e.Status == "Active");

            //inactive
            var inactive = await _db.Employees.CountAsync(e => e.Status == "Inactive");

            var departments = await _db.Employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            var recent = await _db.Employees
                .OrderByDescending(e => e.CreatedAt)
                .Take(5)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Department = e.Department,
                    Designation = e.Designation,
                    Status = e.Status
                })
                .ToListAsync();

            return Ok(new
            {
                totalEmployees = total,
                activeEmployees = active,
                inactiveEmployees = inactive,
                departmentBreakdown = departments,
                recentEmployees = recent
            });
        }
    }
}