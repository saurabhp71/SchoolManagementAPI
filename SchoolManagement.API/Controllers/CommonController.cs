using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Application.Employees.Models.RequestBody;
using SchoolManagement.Application.Employees.Services;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("GetCountriesForDropdown")]
        public async Task<IActionResult> GetCountriesForDropdownAsync()
        {
            var response = await _commonService.GetCountriesForDropdownAsync();

            return Ok(response);
        }

        [HttpGet("GetStateForDropdown")]
        public async Task<IActionResult> GetStateForDropdownAsync(int CountryId)
        {
            var response = await _commonService.GetStateForDropdownAsync(CountryId);

            return Ok(response);
        }

        [HttpGet("GetCityForDropdown")]
        public async Task<IActionResult> GetCityForDropdownAsync(int StateId)
        {
            var response = await _commonService.GetCityForDropdownAsync(StateId);

            return Ok(response);
        }

    }
}
