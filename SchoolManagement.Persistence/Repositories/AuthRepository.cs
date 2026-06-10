using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Auth.RequestsandRequests;
using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Common.CommonServices;
using SchoolManagement.Application.Common.Helpers;
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
    public class AuthRepository : IAuthRepository
    {
        private readonly SchoolDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthRepository(SchoolDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                string password = PasswordHelper.HashPassword(request.Password!);

                var employee = await _context.Employees.Include(x => x.Role)
                    .FirstOrDefaultAsync(x =>
                        x.UserName == request.UserName &&
                        x.IsDeleted != true);

                if (employee == null)
                {
                    return new ApiResponse<LoginResponse>
                    {
                        Success = false,
                        Message = "Invalid username."
                    };
                }



                if (employee.Password != password)
                {
                    return new ApiResponse<LoginResponse>
                    {
                        Success = false,
                        Message = "Invalid password."
                    };
                }

                var accessToken = _jwtTokenService.GenerateToken(
                                    employee.Id,
                                    employee.UserName!,
                                    employee.Role!.Name);

                var refreshToken = _jwtTokenService.GenerateRefreshToken();

                var session = new UserSession
                {
                    EmployeeId = employee.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1),
                    IsRevoked = false
                };

                await _context.UserSessions.AddAsync(session);

                await _context.SaveChangesAsync();

                return new ApiResponse<LoginResponse>
                {
                    Success = true,
                    Message = "Login successful.",
                    Data = new LoginResponse
                    {
                        EmployeeId = employee.Id,
                        EmployeeCode = employee.EmployeeCode,
                        FullName = employee.FullName,
                        UserName = employee.UserName,
                        RoleName = employee.Role.Name,
                        Email = employee.Email,
                        Token = accessToken,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed. {ex.Message}", ex);
            }
        }

        public async Task<ApiResponse<TokenResponse>>RefreshTokenAsync(RefreshTokenRequest request)
        {
            try
            {
                var session = await _context.UserSessions.Include(x => x.Employee).ThenInclude(x => x.Role)
                                .FirstOrDefaultAsync(x =>
                                x.RefreshToken == request.RefreshToken
                                && !x.IsRevoked);

                if (session == null)
                {
                    return new ApiResponse<TokenResponse>
                    {
                        Success = false,
                        Message = "Invalid refresh token."
                    };
                }

                if (session.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    return new ApiResponse<TokenResponse>
                    {
                        Success = false,
                        Message = "Refresh token expired."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return new ApiResponse<TokenResponse>
                    {
                        Success = false,
                        Message = "Refresh token is required."
                    };
                }

                // Generate New JWT
                var accessToken = _jwtTokenService.GenerateToken(
                                        session.Employee.Id,
                                        session.Employee.UserName!,
                                        session.Employee.Role!.Name);

                // Generate New Refresh Token
                var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

                // Update Session
                session.RefreshToken = newRefreshToken;

                session.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

                await _context.SaveChangesAsync();

                return new ApiResponse<TokenResponse>
                {
                    Success = true,
                    Message = "Token refreshed successfully.",
                    Data = new TokenResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = newRefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error refreshing token. {ex.Message}",
                    ex);
            }
        }

        public async Task<ApiResponse<bool>> LogoutAsync(LogoutRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    request.RefreshToken))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Refresh token is required."
                    };
                }

                var session = await _context.UserSessions
                    .FirstOrDefaultAsync(x =>
                        x.RefreshToken ==
                        request.RefreshToken &&
                        !x.IsRevoked);

                if (session == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Session not found."
                    };
                }

                session.IsRevoked = true;

                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Logout successful.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error during logout. {ex.Message}",
                    ex);
            }
        }
    }
}
