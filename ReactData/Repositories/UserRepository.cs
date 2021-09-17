using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactData.Data;
using ReactData.Models;

namespace ReactData.Repositories
{
    public class UserRepository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                return await _context.SaveChangesAsync() >= 1;
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
                var countUsers = _context.Users.Count();

                await _context.Users.AddRangeAsync(users.Select(user => { user.ID = ++countUsers; return user; }).ToList());
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}