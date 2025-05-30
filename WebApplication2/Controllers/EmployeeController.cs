using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllEmployees()
        {
            var employeelist = _context.Employees.ToList();
            return Ok(employeelist);
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            var employeeentity = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
            };
            _context.Employees.Add(employeeentity);
            _context.SaveChanges();
            return Ok(employeeentity);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            _context.SaveChanges();
            return Ok(employee);

        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }
    }
}
