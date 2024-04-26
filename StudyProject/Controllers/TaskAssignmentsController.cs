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
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список назначений.</returns>
        [HttpGet]
        public async Task<List<TaskAssignment>> GetAllTaskAssignment(CancellationToken cancellationToken)
        {
            return await _taskAssignmentRepository.GetAllTaskAssignmentsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить назначение по ID.
        /// </summary>
        /// <param name="id">Идентификатор назначения.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Назнаечние по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignment>> GetTaskAssignmentById(int id, CancellationToken cancellationToken)
        {
            var _assignment = await _taskAssignmentRepository.GetTaskAssignmentByIdAsync(id, cancellationToken);
            if (_assignment == null)
            {
                return NotFound();
            }
            return _assignment;
        }

        /// <summary>
        /// Добавить новое назначение задачи.
        /// </summary>
        /// <param name="newTaskAssignment">Новое назначение задачи.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленное назначение.</returns>
        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> AddTaskAssignmentAsync(TaskAssignment newTaskAssignment, CancellationToken cancellationToken)
        {
            await _taskAssignmentRepository.AddTaskAssignmentAsync(newTaskAssignment, cancellationToken);
            return CreatedAtAction(nameof(GetTaskAssignmentById), new { id = newTaskAssignment.Id }, newTaskAssignment);
        }

        /// <summary>
        /// Обновить существующее назначение по ID.
        /// </summary>
        /// <param name="id">Идентификатор назначения.</param>
        /// <param name="updatedTaskAssignment">Обновленное назначение.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAssignment(int id, TaskAssignment updatedTaskAssignment, CancellationToken cancellationToken)
        {
            if (id != updatedTaskAssignment.Id)
            {
                return BadRequest();
            }
            await _taskAssignmentRepository.UpdateTaskAssignmentAsync(updatedTaskAssignment, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить назначение по ID.
        /// </summary>
        /// <param name="id">Идентификатор назначения задачи.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id, CancellationToken cancellationToken)
        {
            var assignmentToDelete = await _taskAssignmentRepository.GetTaskAssignmentByIdAsync(id, cancellationToken);
            if (assignmentToDelete == null)
            {
                return NotFound();
            }
            await _taskAssignmentRepository.DeleteTaskAssignmentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
