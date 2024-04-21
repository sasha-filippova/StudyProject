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
    public class RolesController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly RoleRepository _roleRepository;

        //public RolesController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}

        public RolesController(RoleRepository roleRepository)
        {
            //_context = context;
            _roleRepository = roleRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Role>> GetAllRole()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var _comment = await _roleRepository.GetRolesById(id);
            if (_comment == null)
            {
                return NotFound();
            }
            return _comment;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> AddRoleAsync(Role newRole)
        {
            await _roleRepository.AddRolesAsync(newRole);
            return CreatedAtAction(nameof(GetRoleById), new { id = newRole.RoleId }, newRole);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role updatedRole)
        {
            if (id != updatedRole.RoleId)
            {
                return BadRequest();
            }
            await _roleRepository.UpdateRoles(updatedRole);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var roleToDelete = await _roleRepository.GetRolesById(id);
            if (roleToDelete == null)
            {
                return NotFound();
            }
            await _roleRepository.DeleteRoles(id);
            return NoContent();
        }
    }
}
