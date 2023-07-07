using System.ComponentModel.DataAnnotations;

namespace myteam.holiday.WebApi.Models
{
    public class UserLoginDto
    {
        [Required, EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string? UserEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}