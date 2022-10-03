using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myteam.holiday.WebServer.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Hash { get; set; }
        [Required]
        public string? Salt { get; set; }
    }
}
