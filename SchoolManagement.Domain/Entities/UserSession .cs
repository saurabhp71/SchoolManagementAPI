using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class UserSession : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public bool IsRevoked { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
