using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class City : BaseEntity
    {
        public string? CityName { get; set; }

        public int StateId { get; set; }

        public State State { get; set; } = null!;
    }
}
