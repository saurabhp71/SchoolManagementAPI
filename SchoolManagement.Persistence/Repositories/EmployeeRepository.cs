using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SchoolDbContext _context;
        public EmployeeRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<RoleDropdownResponse>>> GetRolesForDropdownAsync()
        {
            try
            {
                var roles = await _context.Role
                        .AsNoTracking()
                        .Where(x => x.IsDeleted != true)
                        .Select(x => new RoleDropdownResponse
                        {
                            RoleId = x.Id,
                            RoleName = x.Name
                        }).ToListAsync();

                return new ApiResponse<List<RoleDropdownResponse>>
                {
                    Success = true,
                    Message = "Roles fetched successfully.",
                    Data = roles
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching roles. {ex.Message}", ex);
            }
        }
        public async Task<ApiResponse<bool>> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                var emailExists = await _context.Employees.AnyAsync(x => x.Email == employee.Email && x.IsDeleted != true);

                if (emailExists)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Email already exists.",
                        Data = false
                    };
                }

                var mobileExists = await _context.Employees.AnyAsync(x => x.PhoneNumber == employee.PhoneNumber && x.IsDeleted != true);

                if (mobileExists)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Mobile number already exists.",
                        Data = false
                    };
                }

                var usernameExists = await _context.Employees.AnyAsync(x => x.UserName == employee.UserName && x.IsDeleted != true);

                if (usernameExists)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "User Name already exists.",
                        Data = false
                    };
                }

                var maxId = await _context.Employees.MaxAsync(x => (int?)x.Id) ?? 0;

                var nextNumber = maxId + 1;

                employee.EmployeeCode = $"EMP{DateTime.Now:ddMMyyyy}{nextNumber:D4}";

                employee.CreatedDate = DateTime.Now;
                employee.CreatedBy = "1";
                employee.IsActive = true;
                employee.IsDeleted = false;

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Employee created successfully.",
                    Data = true
                };
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
                var query = _context.Employees
                    .AsNoTracking()
                    .Include(x => x.Role)
                    .Where(x => x.IsDeleted != true);

                // Search
                if (!string.IsNullOrWhiteSpace(request.SearchText))
                {
                    string search = request.SearchText.Trim();

                    query = query.Where(x =>
                        (x.FullName != null && x.FullName.Contains(search)) ||
                        (x.Email != null && x.Email.Contains(search)) ||
                        (x.PhoneNumber != null && x.PhoneNumber.Contains(search)) ||
                        (x.EmployeeCode != null && x.EmployeeCode.Contains(search)) ||
                        (x.Role != null && x.Role.Name.Contains(search)));
                }

                int totalRecords = await query.CountAsync();

                var employees = await query
                    .OrderByDescending(x => x.Id)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new EmployeeListResponse
                    {
                        Id = x.Id,
                        EmployeeCode = x.EmployeeCode,
                        FullName = x.FullName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        RoleId = x.RoleId,
                        RoleName = x.Role != null ? x.Role.Name : string.Empty
                    })
                    .ToListAsync();

                return new ApiResponse<PagedResponse<EmployeeListResponse>>
                {
                    Success = true,
                    Message = "Employee list fetched successfully.",
                    Data = new PagedResponse<EmployeeListResponse>
                    {
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize,
                        TotalRecords = totalRecords,
                        Data = employees
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employees. {ex.Message}", ex);
            }
        }

        public async Task<ApiResponse<EmployeeDetailsResponse>>GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees
                    .AsNoTracking()
                    .Include(x => x.Role)
                    .Include(e => e.City)
                        .ThenInclude(s => s.State)
                        .ThenInclude(c => c.Country)
                    .Where(x => x.Id == employeeId && x.IsDeleted != true)
                    .Select(x => new EmployeeDetailsResponse
                    {
                        Id = x.Id,
                        EmployeeCode = x.EmployeeCode,
                        FullName = x.FullName,
                        Gender = x.Gender,

                        RoleId = x.RoleId,
                        RoleName = x.Role != null? x.Role.Name: string.Empty,

                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        UserName = x.UserName,

                        CurrentAddress = x.CurrentAddress,
                        PermanentAddress = x.PermanentAddress,

                        AadharNumber = x.AadharNumber,
                        PanNumber = x.PanNumber,

                        JoiningDate = x.JoiningDate,
                        DateOfBirth = x.DateOfBirth,

                        CountryId = x.City.State.Country.Id,
                        CountryName = x.City.State.Country.CountryName,
                        StateId = x.City.State.Id,
                        StateName = x.City.State.StateName,
                        CityId = x.CityId,
                        CityName = x.City.CityName,

                        IsActive = x.IsActive
                    }).FirstOrDefaultAsync();

                if (employee == null)
                {
                    return new ApiResponse<EmployeeDetailsResponse>
                    {
                        Success = false,
                        Message = "Employee not found."
                    };
                }

                return new ApiResponse<EmployeeDetailsResponse>
                {
                    Success = true,
                    Message = "Employee details fetched successfully.",
                    Data = employee
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error fetching employee details. {ex.Message}",
                    ex);
            }
        }

        public async Task<ApiResponse<bool>> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId && x.IsDeleted != true);

                if (employee == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = false
                    };
                }

                employee.IsDeleted = true;
                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "1";

                _context.Employees.Update(employee);

                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Employee deleted successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error deleting employee. {ex.Message}",
                    ex);
            }
        }

        public async Task<ApiResponse<bool>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted != true);

                if (employee == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = false
                    };
                }

                // Email Validation
                bool emailExists = await _context.Employees.AnyAsync(x => x.Id != request.Id && x.Email == request.Email && x.IsDeleted != true);

                if (emailExists)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Email already exists.",
                        Data = false
                    };
                }

                // Mobile Validation
                bool mobileExists = await _context.Employees.AnyAsync(x => x.Id != request.Id && x.PhoneNumber == request.PhoneNumber && x.IsDeleted != true);

                if (mobileExists)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Mobile number already exists.",
                        Data = false
                    };
                }

                employee.FullName = request.FullName;
                employee.Gender = request.Gender;
                employee.DateOfBirth = request.DateOfBirth;
                employee.PhoneNumber = request.PhoneNumber;
                employee.Email = request.Email;

                employee.CurrentAddress = request.CurrentAddress;
                employee.PermanentAddress = request.PermanentAddress;

                employee.AadharNumber = request.AadharNumber;
                employee.PanNumber = request.PanNumber;

                employee.CityId = request.CityId;

                employee.JoiningDate = request.JoiningDate;
                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "1";

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Employee updated successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error updating employee. {ex.Message}",
                    ex);
            }
        }

        public async Task<ApiResponse<bool>> ChangeEmployeeStatusAsync(ChangeEmployeeStatusRequest request)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId && x.IsDeleted != true);

                if (employee == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = false
                    };
                }

                employee.IsActive = request.IsActive;

                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "1";

                _context.Employees.Update(employee);

                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = request.IsActive
                        ? "Employee activated successfully."
                        : "Employee deactivated successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error changing employee status. {ex.Message}",
                    ex);
            }
        }
    }
}
