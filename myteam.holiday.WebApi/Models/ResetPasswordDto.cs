using System.ComponentModel.DataAnnotations;

namespace myteam.holiday.WebApi.Models
{
    public class ResetPasswordDto
    {
        public string? Email { get; set; }
        public string? Token { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}