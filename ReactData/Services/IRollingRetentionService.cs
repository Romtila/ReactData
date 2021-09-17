using System.Collections.Generic;
using System.Threading.Tasks;
using ReactData.Models;

namespace ReactData.Services
{
    public interface IRollingRetentionService
    {
        Task<List<RollingRetentionXDay>> GetRollingRetentionXDayFromClient(List<User> users);
        Task<List<RollingRetentionXDay>> GetRollingRetentionXDayFromDB();
        Task<List<RollingRetention7Day>> GetRollingRetention7DayFromClient(List<User> users);
        Task<List<RollingRetention7Day>> GetRollingRetention7DayFromDB();
    }
}