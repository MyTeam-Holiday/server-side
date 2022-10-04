using myteam.holiday.Domain.Models;

namespace myteam.holiday.WebApi.Services
{
    public class ModelValidationService
    {
        public bool IsValidUserModel(User? user)
        {
            return (
                user != null &&
                !string.IsNullOrEmpty(user.UserEmail) &&
                !string.IsNullOrEmpty(user.PasswordHash) &&
                !string.IsNullOrEmpty(user.PasswordSalt));
        }
    }
}
