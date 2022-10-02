using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Medical: DefaultObject
    {
        public string? MedicalName { get; set; }
        public User? User { get; set; }
        public DateTime? MedicalDateFrom { get; set; }
        public DateTime? MedicalDateTo { get; set; }

    }
}
