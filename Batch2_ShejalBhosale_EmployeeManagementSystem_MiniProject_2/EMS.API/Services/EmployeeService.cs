using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public async Task<PagedResult<EmployeeResponseDto>> GetEmployees(EmployeeQueryParams query)
        {
            var employees = _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                employees = employees.Where(e =>
                    (e.FirstName + " " + e.LastName).ToLower().Contains(query.Search.ToLower()) ||
                    e.Email.ToLower().Contains(query.Search.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.Department))
                employees = employees.Where(e => e.Department == query.Department);

            if (!string.IsNullOrWhiteSpace(query.Status))
                employees = employees.Where(e => e.Status == query.Status);

           
            employees = query.SortBy?.ToLower() switch
            {
                "salary" => query.SortDir == "desc"
                    ? employees.OrderByDescending(e => e.Salary)
                    : employees.OrderBy(e => e.Salary),

                "joindate" => query.SortDir == "desc"
                    ? employees.OrderByDescending(e => e.JoinDate)
                    : employees.OrderBy(e => e.JoinDate),

                _ => query.SortDir == "desc"
                    ? employees.OrderByDescending(e => e.FirstName)
                    : employees.OrderBy(e => e.FirstName)
            };

            var totalCount = await employees.CountAsync();

            var data = await employees
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Department = e.Department,
                    Designation = e.Designation,
                    Salary = e.Salary,
                    JoinDate = e.JoinDate,
                    Status = e.Status
                })
                .ToListAsync();

            return new PagedResult<EmployeeResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

     
        public async Task<EmployeeResponseDto?> GetById(int id)
        {
            var e = await _repo.GetByIdAsync(id);

            if (e == null) return null;

            return new EmployeeResponseDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Department = e.Department,
                Designation = e.Designation,
                Salary = e.Salary,
                JoinDate = e.JoinDate,
                Status = e.Status
            };
        }

        public async Task<EmployeeResponseDto> Create(EmployeeRequestDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
                throw new Exception("EMAIL_EXISTS");

            var emp = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoinDate = dto.JoinDate,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(emp);
            await _repo.SaveAsync();

            return await GetById(emp.Id) ?? throw new Exception();
        }

        public async Task<EmployeeResponseDto?> Update(int id, EmployeeRequestDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return null;

            if (await _repo.EmailExistsAsync(dto.Email, id))
                throw new Exception("EMAIL_EXISTS");

            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.Email = dto.Email;
            emp.Phone = dto.Phone;
            emp.Department = dto.Department;
            emp.Designation = dto.Designation;
            emp.Salary = dto.Salary;
            emp.JoinDate = dto.JoinDate;
            emp.Status = dto.Status;
            emp.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(emp);
            await _repo.SaveAsync();

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return false;

            await _repo.DeleteAsync(emp);
            await _repo.SaveAsync();

            return true;
        }
    }
}