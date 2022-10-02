using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Celebration: DefaultObject
    {
        public string? CelebrationName { get; set; }
        public DateTime? CelebrationDateFrom { get; set; }
        public DateTime? CelebrationDateTo { get; set; }
    }
}
