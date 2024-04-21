using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyProject;
using StudyProject.Models;
using StudyProject.Repositories;

namespace StudyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        //public UsersController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}
        private readonly UserRepository _userRepository;


        public UsersController(UserRepository userRepository)
        {
            //_context = context;
            _userRepository = userRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<User>> GetAllUser()
        {
            return await _userRepository.GetAllUsersAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var _user = await _userRepository.GetUsersById(id);
            if (_user == null)
            {
                return NotFound();
            }
            return _user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User newUser)
        {
            await _userRepository.AddUsersAsync(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUsers(updatedUser);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var commentToDelete = await _userRepository.GetUsersById(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUsers(id);
            return NoContent();
        }
    }
}
