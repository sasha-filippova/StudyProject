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
    /// Контроллер для управления задачами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

       
        private readonly TaskRepository _taskRepository;

        /// <summary>
        /// Конструктор для TasksController.
        /// </summary>
        /// <param name="taskRepository">Репозиторий задач.</param>
        public TasksController(TaskRepository taskRepository)
        {

            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Получить все задачи.
        /// </summary>
        [HttpGet]
        public async Task<List<Models.Task>> GetAllTask(CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllTasksAsync(cancellationToken);
        }

        /// <summary>
        /// Получить задачу по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTasksById(int id, CancellationToken cancellationToken)
        {
            var _task = await _taskRepository.GetTaskByIdAsync(id, cancellationToken);
            if (_task == null)
            {
                return NotFound();
            }
            return _task;
        }

        /// <summary>
        /// Добавить новую задачу.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Models.Task>> AddTasksAsync(Models.Task newTask, CancellationToken cancellationToken)
        {
            await _taskRepository.AddTaskAsync(newTask, cancellationToken);
            return CreatedAtAction(nameof(GetTasksById), new { id = newTask.TaskId }, newTask);
        }

        /// <summary>
        /// Обновить существующую задачу по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasks(int id, Models.Task updatedTask, CancellationToken cancellationToken)
        {
            if (id != updatedTask.TaskId)
            {
                return BadRequest();
            }
            await _taskRepository.UpdateTaskAsync(updatedTask, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить задача по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id, CancellationToken cancellationToken)
        {
            var taskToDelete = await _taskRepository.GetTaskByIdAsync(id, cancellationToken);
            if (taskToDelete == null)
            {
                return NotFound();
            }
            await _taskRepository.DeleteTaskAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
