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
    public class StatusController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        //public StatusController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}
        private readonly StatusRepository _statusRepository;

        public StatusController(StatusRepository statusRepository)
        {
            //_context = context;
            _statusRepository = statusRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Status>> GetAllStatuses()
        {
            return await _statusRepository.GetAllStatusesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id)
        {
            var _status = await _statusRepository.GetStatusesById(id);
            if (_status == null)
            {
                return NotFound();
            }
            return _status;
        }

        [HttpPost]
        public async Task<ActionResult<Status>> AddStatusAsync(Status newStatus)
        {
            await _statusRepository.AddStatusesAsync(newStatus);
            return CreatedAtAction(nameof(GetStatusById), new { id = newStatus.StatusId }, newStatus);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, Status updatedStatus)
        {
            if (id != updatedStatus.StatusId)
            {
                return BadRequest();
            }
            await _statusRepository.UpdateStatuses(updatedStatus);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var statusToDelete = await _statusRepository.GetStatusesById(id);
            if (statusToDelete == null)
            {
                return NotFound();
            }
            await _statusRepository.DeleteStatuses(id);
            return NoContent();
        }
    }
}
