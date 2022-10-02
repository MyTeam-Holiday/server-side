using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Weekend: DefaultObject
    {
        public string? WeekendName { get; set; }
        public DateTime? WeekendDateFrom { get; set; }
        public DateTime? WeekendDateTo { get; set; }
    }
}
