using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactData.Models;

namespace ReactData.Repositories
{
    public interface IRepository : IDisposable
    {
        Task<List<User>> GetUserList();

        Task<bool> AddUser(User user);

        Task<bool> AddUsers(List<User> users);
    }
}