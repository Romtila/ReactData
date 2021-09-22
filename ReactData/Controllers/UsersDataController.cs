using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReactData.Data;
using ReactData.Models;
using ReactData.Repositories;
using ReactData.Services;

namespace ReactData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersDataController : Controller
    {
        private readonly IUserService _userService;

        public UsersDataController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(List<User> users)
        {
            return await _userService.AddUsers(users) ? Ok() : StatusCode(500);
        }
    }
}
