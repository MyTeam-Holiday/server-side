using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.Domain.Models
{
    public class User : DefaultObject
    {
        public string? Username { get; set; }
        public string? UserEmail { get; set; }
        public string? PasswordHash { get; set; }
        public UserRole? UserRole { get; set; }
        public bool CreateOpportunity { get; set; }
    }
}
