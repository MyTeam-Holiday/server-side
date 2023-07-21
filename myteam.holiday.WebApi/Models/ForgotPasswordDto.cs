using System.ComponentModel.DataAnnotations;

namespace myteam.holiday.WebApi.Models
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string? Email { get; set; }
    }
}