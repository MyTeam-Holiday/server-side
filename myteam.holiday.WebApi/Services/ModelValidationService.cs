using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myteam.holiday.Domain.Models;
using myteam.holiday.EntityFramework.Services;
using System.Text;

namespace myteam.holiday.WebApi.Services
{
    public class ModelValidationService
    {
        private readonly UserAppDbService _userDbService;
        public ModelValidationService(UserAppDbService userDbService)
        {
            _userDbService = userDbService;
        }

        public bool IsValidUserModel(User? user)
        {
            return (
                user != null &&
                !string.IsNullOrEmpty(user.UserEmail) &&
                !string.IsNullOrEmpty(user.PasswordHash) &&
                !string.IsNullOrEmpty(user.PasswordSalt));
        }

        public async Task<bool> IsCanJoinUserInAccount(string email, string password)
        {
            User? user = await _userDbService.GetOneEmailAsync(email);
            if (user == null) return false;
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.Unicode.GetBytes(user.PasswordSalt!),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return await _userDbService.IsConvergeUserHashAsync(email, hashed);
        }

        public async Task<bool> IsCanCreateUserAccount(User user)
        {
            return (
                !string.IsNullOrEmpty(user.UserEmail) &&
                await _userDbService.GetOneEmailAsync(user.UserEmail!) == null);
        }

        public async Task<bool> IsCanUpdateUserAccount(User user, string hash)
        {
            return (
                !string.IsNullOrEmpty(user.UserEmail) &&
                await _userDbService.GetOneEmailAsync(user.UserEmail!) != null &&
                await _userDbService.IsConvergeUserHashAsync(user.UserEmail!, hash));
        }
    }
}
