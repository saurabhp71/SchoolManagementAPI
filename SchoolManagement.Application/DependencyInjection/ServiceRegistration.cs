using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Common.CommonServices;
using SchoolManagement.Application.Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection
        AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
