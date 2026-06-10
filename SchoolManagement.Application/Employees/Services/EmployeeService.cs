using SchoolManagement.Application.Common.Helpers;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Application.Employees.Models.RequestBody;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ApiResponse<List<RoleDropdownResponse>>> GetRolesForDropdownAsync()
        {
            try
            {
                return await _employeeRepository.GetRolesForDropdownAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApiResponse<bool>> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Full Name is required.",
                        Data = false
                    };
                }

                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "User Name is required.",
                        Data = false
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Email is required.",
                        Data = false
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Password is required.",
                        Data = false
                    };
                }

                var employee = new Employee
                {
                    FullName = request.FullName,
                    RoleId = request.RoleId,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    UserName = request.UserName,
                    Password = PasswordHelper.HashPassword(request.Password!)
                };

                return await _employeeRepository.CreateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating employee. {ex.Message}", ex);
            }
        }
        public async Task<ApiResponse<PagedResponse<EmployeeListResponse>>> GetAllEmployeeAsync(GetEmployeeListRequest request)
        {
            try
            {
                return await _employeeRepository.GetAllEmployeeAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<EmployeeDetailsResponse>> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                return await _employeeRepository.DeleteEmployeeAsync(employeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            try
            {
                return await _employeeRepository.UpdateEmployeeAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error: {ex.Message}",
                    ex);
            }
        }

        public async Task<ApiResponse<bool>> ChangeEmployeeStatusAsync(ChangeEmployeeStatusRequest request)
        {
            try
            {
                return await _employeeRepository.ChangeEmployeeStatusAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error: {ex.Message}",
                    ex);
            }
        }
    }
}
