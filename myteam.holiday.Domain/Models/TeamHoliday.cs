using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class TeamHoliday
    {
        [Key]
        [Column("GuId")]
        public Guid GuId { get; set; }
        public Guid HolidayGuId { get; set; }
        public Guid TeamGuId { get; set; }
        
    }
}