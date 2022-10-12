using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Team
    {   
        [Key]
        [Column("GuId")]
        public Guid GuId { get; set; }
        public string? TeamName { get; set; }
        [Column("UuidFromBin(BaseGuId)")]
        public Guid BaseGuId { get; set; }
        public string? PasswordSalt { get; set; }
        public string? PasswordHash { get; set; }
        public string? InviteLink { get; set; }
        public bool HasPrivate { get; set; }
        public bool OnlyInvite { get; set;}
        
    }
}
