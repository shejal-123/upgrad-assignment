using Microsoft.EntityFrameworkCore;
using EMS.API.Data;

namespace EMS.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Employee> GetAll()
        {
            return _db.Employees.AsQueryable();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task AddAsync(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            _db.Employees.Remove(employee);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _db.Employees.AnyAsync(e =>
                e.Email.ToLower() == email.ToLower() &&
                (excludeId == null || e.Id != excludeId));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}