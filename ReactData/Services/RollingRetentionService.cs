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
        /*
        
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
        */
        public async Task<List<RollingRetentionXDay>> GetRollingRetention7DayFromDB()
        {
            try
            {
                return CalculateRollingRetention7Day(await _userRepository.GetUserList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<RollingRetentionXDay>();
            }
        }

        public List<RollingRetentionXDay> CalculateRollingRetention7Day(List<User> users)
        {
            try
            {
                var list = users.Select(user => new RollingRetention7Day() { Id = user.ID, UserLifespan = (user.DateLastActivity - user.DateRegistration).Days }).ToList();

                var query = list.GroupBy(user => user.UserLifespan, user => user.Id);

                return query.Select(user => new RollingRetentionXDay() { UserLifespan = user.Key, Count = user.Count() }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<double> GetRollingRetention()
        {
            try
            {
                var users = await _userRepository.GetUserList();

                return CalculateRollingRetention(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new double();
            }
        }

        public double CalculateRollingRetention(List<User> users)
        {
            try
            {
                var lastActivityListUsers = new List<User>();
                var registrationListUsers = new List<User>();

                lastActivityListUsers = users.Where(user => user.DateLastActivity >= DateTime.Today.AddDays(-7)).ToList();
                registrationListUsers = users.Where(user => user.DateRegistration <= DateTime.Today.AddDays(-7)).ToList();

                var result = ((double)lastActivityListUsers.Count / (double)registrationListUsers.Count) * 100;

                return Math.Round(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}