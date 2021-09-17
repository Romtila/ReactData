using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactData.Models;
using ReactData.Repositories;

namespace ReactData.Services
{
    public class RollingRetentionService : IRollingRetentionService
    {
        private readonly IRepository _userRepository;
        private readonly ILogger _logger;

        private const int maxDay = 32;

        public RollingRetentionService(IRepository userRepository, ILogger<RollingRetentionService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public async Task<List<RollingRetentionXDay>> GetRollingRetentionXDayFromClient(List<User> users)
        {
            try
            {
                return CalculateRollingRetentionXDay(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<RollingRetentionXDay>();
            }
        }

        public async Task<List<RollingRetentionXDay>> GetRollingRetentionXDayFromDB()
        {
            try
            {
                return CalculateRollingRetentionXDay(await _userRepository.GetUserList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<RollingRetentionXDay>();
            }
        }

        public async Task<List<RollingRetention7Day>> GetRollingRetention7DayFromClient(List<User> users)
        {
            try
            {
                return CalculateRollingRetention7Day(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<RollingRetention7Day>();
            }
        }

        public async Task<List<RollingRetention7Day>> GetRollingRetention7DayFromDB()
        {
            try
            {
                return CalculateRollingRetention7Day(await _userRepository.GetUserList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<RollingRetention7Day>();
            }
        }

        public List<RollingRetentionXDay> CalculateRollingRetentionXDay(List<User> users)
        {
            try
            {
                var data = new List<RollingRetentionXDay>();
                double countInstall = users.Count();

                for (var i = 0; i < maxDay; i++)
                {
                    double countBack = users.Count(user => user.DateRegistration.AddDays(i) <= user.DateLastActivity);
                    if (countBack == 0)
                        return data;

                    data.Add(new RollingRetentionXDay()
                    {
                        Day = i,
                        Percent = (countBack / countInstall) * 100
                    });
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RollingRetention7Day> CalculateRollingRetention7Day(List<User> users)
        {
            try
            {
                return users.Select(user => new RollingRetention7Day() { Id = user.ID, UserLifespan = (user.DateLastActivity - user.DateRegistration).Days }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}