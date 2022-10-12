using Microsoft.AspNetCore.Mvc;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;

namespace myteam.holiday.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost, Route("PreCreateUser")]
        public async Task<string> PreCreateUser(string userName, string userEmail)
        {
            return await _userRepository.PreCreateUser(userName, userEmail);
        }
        [HttpPost, Route("CreateUser")]
        public async Task<int> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }
        [HttpGet, Route("GetUserByGuId")]
        public async Task<User> GetUserById(string guId)
        {
            return await _userRepository.GetUserByGuId(guId);
        }
        [HttpGet, Route("GetUserByEmail")]
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        [HttpDelete, Route("DeleteUser")]
        public async Task<bool> DeleteUser(string guId)
        {
            return await _userRepository.DeleteUser(guId);
        }
        [HttpGet, Route("GetUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
        [HttpPut, Route("UpdateUser")]
        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}