using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Employees.Models.DTO_s
{
    public class EmployeeDetailsResponse
    {
        public int Id { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FullName { get; set; }

        public string? Gender { get; set; }

        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public int? CountryId { get; set; }

        public string? CountryName { get; set; }

        public int? StateId { get; set; }

        public string? StateName { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public DateTime JoiningDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
