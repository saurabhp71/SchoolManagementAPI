using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Application.Employees.Models.RequestBody;
using SchoolManagement.Application.Employees.Services;


namespace SchoolManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService  = employeeService;
        }

        [HttpGet("GetRolesForDropdown")]
        public async Task<IActionResult> GetRolesForDropdownAsync()
        {
            var response = await _employeeService.GetRolesForDropdownAsync();

            return Ok(response);
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest request)
        {
            var response =
                await _employeeService.CreateEmployeeAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("GetAllEmployeeList")]
        public async Task<IActionResult> GetAllEmployeeAsync([FromBody] GetEmployeeListRequest request)
        {
            var response = await _employeeService.GetAllEmployeeAsync(request);

            return Ok(response);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int employeeId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeId);

            return Ok(response);
        }

        [HttpGet("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeAsync(int employeeId)
        {
            var response = await _employeeService.DeleteEmployeeAsync(employeeId);

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            var response = await _employeeService.UpdateEmployeeAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPatch("ChangeEmployeeStatus")]
        public async Task<IActionResult> ChangeEmployeeStatusAsync(ChangeEmployeeStatusRequest request)
        {
            var response =await _employeeService.ChangeEmployeeStatusAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
