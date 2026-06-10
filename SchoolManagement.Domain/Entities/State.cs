using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class State : BaseEntity
    {
        public string? StateName { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;

        public ICollection<City> Cities { get; set; }
            = new List<City>();
    }
}
