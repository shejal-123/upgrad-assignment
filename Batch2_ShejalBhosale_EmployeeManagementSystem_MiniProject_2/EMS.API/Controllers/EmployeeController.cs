using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        // return all employees list
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeQueryParams query)
        {
            var result = await _service.GetEmployees(query);
            return Ok(result);
        }


        //return employee by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetById(id);

            if (emp == null)
                return NotFound();

            return Ok(emp);
        }

        //create employee
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                if (ex.Message == "EMAIL_EXISTS")
                    return Conflict("Email already exists");

                return StatusCode(500, "Server error");
            }
        }
 
        //update employee
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _service.Update(id, dto);

                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex) when (ex.Message == "EMAIL_EXISTS")
            {
                return Conflict("Email already exists");
            }
        }

        //delete employee
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound();

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}