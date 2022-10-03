using myteam.holiday.WebServer.Model;
using myteam.holiday.WebServer.Services;

namespace myteam.holiday.WebApi.Services
{
    public class DbValidationService
    {
        private readonly AppDbGroupService _groupService;
        private readonly AppDbUserService _userService;
        public DbValidationService(
            AppDbGroupService groupService,
            AppDbUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        public bool IsValidUserModel(User user)
        {
            return (
                !string.IsNullOrEmpty(user.Login) &&
                !string.IsNullOrEmpty(user.Hash) &&
                user.Salt != null);
        }

        public async Task<bool> IsConvergeUserPasswords(string login, string hash)
        {
            User? user = await _userService.GetOneLoginAsync(login);
            return (
                user != null &&
                user.Hash == hash);
        }

        public async Task<bool> IsValidUpdateUserModelAsync(User newUser, string hash)
        {
            return (
                IsValidUserModel(newUser) &&
                await _userService.GetOneLoginAsync(newUser.Login!) != null &&
                await IsConvergeUserPasswords(newUser.Login!, hash));
        }

        public async Task<bool> IsValidCreateUserModelAsync(User newUser)
        {
            return (
                IsValidUserModel(newUser) &&
                await _userService.GetOneLoginAsync(newUser.Login!) == null);
        }
    }
}
