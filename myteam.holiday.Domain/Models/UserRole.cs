using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class UserRole
    {
        [Key]
        [Column("GuId")]
        public Guid GuId { get; set; }
        public string? RoleName { get; set; }
        public int RoleStatus { get; set; }
    }
}
