using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactData.Models;
using ReactData.Services;

namespace ReactData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RollingRetentionController : Controller
    {
        private readonly IRollingRetentionService _rollingRetentionService;

        public RollingRetentionController(IRollingRetentionService rollingRetentionService)
        {
            _rollingRetentionService = rollingRetentionService;
        }

        [HttpGet("CalculateRollingRetention")]
        public async Task<double> CalculateRollingRetention()
            => await _rollingRetentionService.GetRollingRetention();
        /*
        [HttpGet("CalculateDataRollingRetentionXDayFromDB")]
        public async Task<List<RollingRetentionXDay>> CalculateRollingRetentionXDayFromDB()
            => await _rollingRetentionService.GetRollingRetentionXDayFromDB();
        
        [HttpPost("CalculateDataRollingRetention7DayFromClient")]
        public async Task<List<RollingRetention7Day>> CalculateRollingRetention7DayFromClient(List<User> users)
            => await _rollingRetentionService.GetRollingRetention7DayFromClient(users);

        [HttpPost("CalculateDataRollingRetentionXDayFromClient")]
        public async Task<List<RollingRetentionXDay>> CalculateRollingRetentionXDayFromClient(List<User> users)
            => await _rollingRetentionService.GetRollingRetentionXDayFromClient(users);*/

        [HttpGet("CalculateDataRollingRetention7DayFromDB")]
        public async Task<List<RollingRetentionXDay>> CalculateRollingRetention7DayFromDB()
            => await _rollingRetentionService.GetRollingRetention7DayFromDB();


    }
}
