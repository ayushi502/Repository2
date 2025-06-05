using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;
        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            var employees = await _repository.GetAllAsync();
            var employeeDtos = employees.Select(s => new EmployeeDto
            {
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone
            });
            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return NotFound();
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
            var employee = await _repository.AddAsync(employeeentity);
            return Ok(employeeentity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeDto dto)
        {
            var update = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var updated = await _repository.UpdateAsync(id, update);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
