using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.Domain.Services
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<bool> DeleteUser(string guId);
        Task<string> PreCreateUser(string userName, string userEmail);
        Task<User> GetUserByGuId(string guId);
        Task<bool> UpdateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsers();
    }
}