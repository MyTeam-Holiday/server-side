using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.WebServer.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int RoleId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
