using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactData.Models;
using ReactData.Repositories;

namespace ReactData.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IRepository userRepository, ILogger<UserService> Logger)
        {
            _userRepository = userRepository;
            _logger = Logger;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                await _userRepository.AddUser(user);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
        public async Task<bool> AddUsers(List<User> users)
        {
            try
            {
                var nUser = users.Select(user => user.ID = 0);
                await _userRepository.AddUsers(users);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUserList();
        }
    }
}