using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Employees.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ApiResponse<List<RoleDropdownResponse>>> GetRolesForDropdownAsync();
        Task<ApiResponse<bool>> CreateEmployeeAsync(Employee employee);
        Task<ApiResponse<PagedResponse<EmployeeListResponse>>>GetAllEmployeeAsync(GetEmployeeListRequest request);
        Task<ApiResponse<EmployeeDetailsResponse>>GetEmployeeByIdAsync(int employeeId);
        Task<ApiResponse<bool>> DeleteEmployeeAsync(int employeeId);
        Task<ApiResponse<bool>> UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<ApiResponse<bool>> ChangeEmployeeStatusAsync(ChangeEmployeeStatusRequest request);
    }
}
