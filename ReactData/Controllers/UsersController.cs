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
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Users")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [Route("SaveUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            return await _userService.AddUser(user) ? Ok() : StatusCode(500);
        }
    }
}
