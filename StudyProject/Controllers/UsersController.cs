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
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список пользователей.</returns>
        [HttpGet]
        public async Task<List<User>> GetAllUser(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersAsync(cancellationToken);
        }

        /// <summary>
        /// Получить пользователя по ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Пользователь по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id, CancellationToken cancellationToken)
        {
            var _user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
            if (_user == null)
            {
                return NotFound();
            }
            return _user;
        }

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="newUser">Новый пользователь.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный пользователь.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User newUser, CancellationToken cancellationToken)
        {
            await _userRepository.AddUserAsync(newUser, cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        /// <summary>
        /// Обновить существующего пользователя по ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="updatedUser">Обновленный пользователь.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser, CancellationToken cancellationToken)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUserAsync(updatedUser, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить пользователя по ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _userRepository.GetUserByIdAsync(id, cancellationToken);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUserAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
