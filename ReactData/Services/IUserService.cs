using System.Collections.Generic;
using System.Threading.Tasks;
using ReactData.Models;

namespace ReactData.Services
{
    public interface IUserService
    {
        public Task<bool> AddUser(User user);

        public Task<bool> AddUsers(List<User> users);
        public Task<List<User>> GetUsers();
    }
}