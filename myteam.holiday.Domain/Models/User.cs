using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class User
    {
        [Key]
        [Column("GuId")]
        public Guid GuId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? PasswordSalt { get; set; }
        public string? PasswordHash { get; set; }
        public int HasVerified { get; set; }
    }
}
