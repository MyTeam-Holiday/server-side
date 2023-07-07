using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace myteam.holiday.WebApi.Models
{
    //надо понаписать настроек как например: мах/min длина, запрещенные символы и т.д.
    public class UserRegDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required, EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string? UserEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}