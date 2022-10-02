using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class Team: DefaultObject
    {
        public string? TeamName { get; set; }
        public string? TeamBase { get; set; }
        public string? InviteLink { get; set; }
        public bool Private { get; set; }
        
    }
}
