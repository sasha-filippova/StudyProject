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
    /// Контроллер для управления назначением задач.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        
        private readonly TaskAssignmentRepository _taskAssignmentRepository;

        /// <summary>
        /// Конструктор для TaskAssignmentsController.
        /// </summary>
        /// <param name="taskAssignmentRepository">Репозиторий назначений.</param>
        public TaskAssignmentsController(TaskAssignmentRepository taskAssignmentRepository)
        {
           
            _taskAssignmentRepository = taskAssignmentRepository;
        }

        /// <summary>
        /// Получить все назначения задач.
        /// </summary>
        [HttpGet]
        public async Task<List<TaskAssignment>> GetAllTaskAssignment(CancellationToken cancellationToken)
        {
            return await _taskAssignmentRepository.GetAllTaskAssignmentsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить назначение по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignment>> GetTaskAssignmentById(int id, CancellationToken cancellationToken)
        {
            var _assignment = await _taskAssignmentRepository.GetTaskAssignmentsByIdAsync(id, cancellationToken);
            if (_assignment == null)
            {
                return NotFound();
            }
            return _assignment;
        }

        /// <summary>
        /// Добавить новое назначение задачи.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> AddTaskAssignmentAsync(TaskAssignment newTaskAssignment, CancellationToken cancellationToken)
        {
            await _taskAssignmentRepository.AddTaskAssignmentsAsync(newTaskAssignment, cancellationToken);
            return CreatedAtAction(nameof(GetTaskAssignmentById), new { id = newTaskAssignment.AssignmentId }, newTaskAssignment);
        }

        /// <summary>
        /// Обновить существующее назначение по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAssignment(int id, TaskAssignment updatedTaskAssignment, CancellationToken cancellationToken)
        {
            if (id != updatedTaskAssignment.AssignmentId)
            {
                return BadRequest();
            }
            await _taskAssignmentRepository.UpdateTaskAssignmentsAsync(updatedTaskAssignment);
            return NoContent();
        }

        /// <summary>
        /// Удалить назначение по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id, CancellationToken cancellationToken)
        {
            var assignmentToDelete = await _taskAssignmentRepository.GetTaskAssignmentsByIdAsync(id, cancellationToken);
            if (assignmentToDelete == null)
            {
                return NotFound();
            }
            await _taskAssignmentRepository.DeleteTaskAssignmentsAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
