using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Employees.Models.DTO_s
{
    public class ChangeEmployeeStatusRequest
    {
        public int EmployeeId { get; set; }

        public bool IsActive { get; set; }
    }
}
