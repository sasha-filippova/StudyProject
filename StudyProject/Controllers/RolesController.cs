using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    /// Контроллер для управления ролями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly RoleRepository _roleRepository;


        /// <summary>
        /// Конструктор для RolesController.
        /// </summary>
        /// <param name="roleRepository">Репозиторий ролей.</param>
        public RolesController(RoleRepository roleRepository)
        {
           
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Получить все роли.
        /// </summary>
        [HttpGet]
        public async Task<List<Role>> GetAllRole(CancellationToken cancellationToken)
        {
            return await _roleRepository.GetAllRolesAsync(cancellationToken);
        }

        /// <summary>
        /// Получить роль по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id, CancellationToken cancellationToken)
        {
            var _comment = await _roleRepository.GetRolesByIdAsync(id, cancellationToken);
            if (_comment == null)
            {
                return NotFound();
            }
            return _comment;
        }

        /// <summary>
        /// Добавить новую роль.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Role>> AddRoleAsync(Role newRole, CancellationToken cancellationToken)
        {
            await _roleRepository.AddRolesAsync(newRole, cancellationToken);
            return CreatedAtAction(nameof(GetRoleById), new { id = newRole.RoleId }, newRole);
        }

        /// <summary>
        /// Обновить существующую роль по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role updatedRole, CancellationToken cancellationToken)
        {
            if (id != updatedRole.RoleId)
            {
                return BadRequest();
            }
            await _roleRepository.UpdateRolesAsync(updatedRole, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить роль по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
        {
            var roleToDelete = await _roleRepository.GetRolesByIdAsync(id, cancellationToken);
            if (roleToDelete == null)
            {
                return NotFound();
            }
            await _roleRepository.DeleteRolesAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
