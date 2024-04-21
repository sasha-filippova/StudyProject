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
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        //public TaskAssignmentsController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}
        private readonly TaskAssignmentRepository _taskAssignmentRepository;

        public TaskAssignmentsController(TaskAssignmentRepository taskAssignmentRepository)
        {
            //_context = context;
            _taskAssignmentRepository = taskAssignmentRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<TaskAssignment>> GetAllTaskAssignment()
        {
            return await _taskAssignmentRepository.GetAllTaskAssignmentsAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignment>> GetTaskAssignmentById(int id)
        {
            var _assignment = await _taskAssignmentRepository.GetTaskAssignmentsById(id);
            if (_assignment == null)
            {
                return NotFound();
            }
            return _assignment;
        }

        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> AddTaskAssignmentAsync(TaskAssignment newTaskAssignment)
        {
            await _taskAssignmentRepository.AddTaskAssignmentsAsync(newTaskAssignment);
            return CreatedAtAction(nameof(GetTaskAssignmentById), new { id = newTaskAssignment.AssignmentId }, newTaskAssignment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAssignment(int id, TaskAssignment updatedTaskAssignment)
        {
            if (id != updatedTaskAssignment.AssignmentId)
            {
                return BadRequest();
            }
            await _taskAssignmentRepository.UpdateTaskAssignments(updatedTaskAssignment);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id)
        {
            var assignmentToDelete = await _taskAssignmentRepository.GetTaskAssignmentsById(id);
            if (assignmentToDelete == null)
            {
                return NotFound();
            }
            await _taskAssignmentRepository.DeleteTaskAssignments(id);
            return NoContent();
        }
    }
}
