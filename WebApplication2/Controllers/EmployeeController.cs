using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbcontext _context;
        public EmployeeController(EmployeeDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult> GetAllEmployees()
        {
            var employeelist = await _context.Employees.ToListAsync();
            return Ok(employeelist);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            var employee =await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            var employeeentity = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
            };
           await _context.Employees.AddAsync(employeeentity);
          await  _context.SaveChangesAsync();
            return Ok(employeeentity);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
           await _context.SaveChangesAsync();
            return Ok(employee);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
           await  _context.SaveChangesAsync();
            return Ok();
        }
    }
}
