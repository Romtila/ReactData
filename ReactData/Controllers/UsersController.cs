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

namespace ReactData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IRepository _repository;

        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        [Route("Users")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetUserList();
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<IActionResult> SaveUser([FromBody] User user)
        {
            return Ok(await _repository.Create(user));
        }
    }
}
