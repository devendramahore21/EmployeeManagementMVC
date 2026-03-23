using EmployeeManagementMVC.DTOs;
using EmployeeManagementMVC.RequestModels;
using EmployeeManagementMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeApiController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employeeapi
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeQueryParams query)
        {
            var result = await _service
                            .GetEmployeesAsync(query.Search, query.SortOrder, query.Page, query.PageSize);

            return Ok(result);
        }

        // GET: api/employeeapi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employeeapi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
        {
            await _service.AddAsync(dto);

            return Ok("Employee created successfully");
        }

        // PUT: api/employeeapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            await _service.UpdateAsync(dto);

            return Ok("Employee updated successfully");
        }

        // DELETE: api/employeeapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Employee deleted successfully");
        }
    }
}
