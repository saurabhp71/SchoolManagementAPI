using SchoolManagement.Application.Auth.RequestsandRequests;
using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Common.CommonServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IAuthRepository authRepository, IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ApiResponse<LoginResponse>>LoginAsync(LoginRequest request)
        {
            var response = await _authRepository.LoginAsync(request);

            return response;
        }

        public async Task<ApiResponse<TokenResponse>>RefreshTokenAsync(RefreshTokenRequest request)
        {
            return await _authRepository.RefreshTokenAsync(request);
        }
        public async Task<ApiResponse<bool>> LogoutAsync(LogoutRequest request)
        {
            return await _authRepository.LogoutAsync(request);
        }
    }
}
