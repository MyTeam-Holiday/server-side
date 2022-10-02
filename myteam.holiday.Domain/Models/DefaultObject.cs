using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class DefaultObject
    {
        [Key]
        public Guid Id { get; set; }
    }
}
