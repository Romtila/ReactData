using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactData.Models;
using ReactData.Services;

namespace ReactData.Controllers
{
    public class RollingRetentionController : Controller
    {
        private readonly IRollingRetentionService _rollingRetentionService;

        public RollingRetentionController(IRollingRetentionService rollingRetentionService)
        {
            _rollingRetentionService = rollingRetentionService;
        }

        [HttpGet("CalculateRollingRetentionXDayFromDB")]
        public async Task<List<RollingRetentionXDay>> CalculateRollingRetentionXDayFromDB()
            => await _rollingRetentionService.GetRollingRetentionXDayFromDB();

        [HttpGet("CalculateRollingRetention7DayFromDB")]
        public async Task<List<RollingRetention7Day>> CalculateRollingRetention7DayFromDB()
            => await _rollingRetentionService.GetRollingRetention7DayFromDB();

        [HttpPost("CalculateRollingRetention7DayFromClient")]
        public async Task<List<RollingRetention7Day>> CalculateRollingRetention7DayFromClient(List<User> users)
            => await _rollingRetentionService.GetRollingRetention7DayFromClient(users);


        [HttpPost("CalculateRollingRetentionXDayFromClient")]
        public async Task<List<RollingRetentionXDay>> CalculateRollingRetentionXDayFromClient(List<User> users)
            => await _rollingRetentionService.GetRollingRetentionXDayFromClient(users);
    }
}
