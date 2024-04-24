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
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

      
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Конструктор для UserRepository.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        public UsersController(UserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        [HttpGet]
        public async Task<List<User>> GetAllUser(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersAsync(cancellationToken);
        }

        /// <summary>
        /// Получить пользователя по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id, CancellationToken cancellationToken)
        {
            var _user = await _userRepository.GetUsersByIdAsync(id, cancellationToken);
            if (_user == null)
            {
                return NotFound();
            }
            return _user;
        }

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User newUser, CancellationToken cancellationToken)
        {
            await _userRepository.AddUsersAsync(newUser, cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }

        /// <summary>
        /// Обновить существующего пользователя по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser, CancellationToken cancellationToken)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUsersAsync(updatedUser, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить пользователя по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _userRepository.GetUsersByIdAsync(id, cancellationToken);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUsersAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
