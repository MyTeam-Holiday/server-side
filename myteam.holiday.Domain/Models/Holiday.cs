using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Holiday
    {
        [Key]
        [Column("GuId")]
        public Guid GuId { get; set; }
        public string? HolidayName { get; set; }
        public int HolidayStatus { get; set; }
        public int HolidayTimeStamp { get; set; }
        public Guid UserGuId { get; set; }
    }
}