using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Interface;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class EmployeeRepository : IEmployeeRepository

    {
        private readonly EmployeeDbcontext _context;
        public EmployeeRepository(EmployeeDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
        public async Task<Employee> AddAsync(Employee employee){
           await _context.Employees.AddAsync(employee);
          await  _context.SaveChangesAsync();
            return employee;

        }
        public async Task<Employee?> UpdateAsync(int id, Employee employee)
        {
            var existingemployee = await _context.Employees.FindAsync(id);
            if (existingemployee == null) return null;

            existingemployee.Name = employee.Name;
            existingemployee.Email = employee.Email;
            existingemployee.Phone = employee.Phone;
            await _context.SaveChangesAsync();

            return existingemployee;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return false;

            _context.Employees.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
