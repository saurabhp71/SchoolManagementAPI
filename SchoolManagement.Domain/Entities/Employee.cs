using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Employee: BaseEntity
    {
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public int RoleId { get; set; }
        public Roles Role { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public DateTime JoiningDate { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; } = null!;
        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    }
}
