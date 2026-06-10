using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Employees.Models.DTO_s
{
    public class RoleDropdownResponse
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;
    }
}
