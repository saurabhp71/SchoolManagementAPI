using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Common.CommonInterface
{
    public interface IJwtTokenService
    {
        string GenerateToken(int employeeId, string userName, string roleName);
        string GenerateRefreshToken();
    }
}
