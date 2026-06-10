using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string? CountryName { get; set; }
        public ICollection<State> States { get; set; }
           = new List<State>();
    }
}
