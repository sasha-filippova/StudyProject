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
    public class TasksController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        //public TasksController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}
        private readonly TaskRepository _taskRepository;


        public TasksController(TaskRepository taskRepository)
        {
            //_context = context;
            _taskRepository = taskRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Models.Task>> GetAllTask()
        {
            return await _taskRepository.GetAllTasksAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTasksById(int id)
        {
            var _task = await _taskRepository.GetTaskById(id);
            if (_task == null)
            {
                return NotFound();
            }
            return _task;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> AddTasksAsync(Models.Task newTask)
        {
            await _taskRepository.AddTaskAsync(newTask);
            return CreatedAtAction(nameof(GetTasksById), new { id = newTask.TaskId }, newTask);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasks(int id, Models.Task updatedTask)
        {
            if (id != updatedTask.TaskId)
            {
                return BadRequest();
            }
            await _taskRepository.UpdateTask(updatedTask);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var taskToDelete = await _taskRepository.GetTaskById(id);
            if (taskToDelete == null)
            {
                return NotFound();
            }
            await _taskRepository.DeleteTask(id);
            return NoContent();
        }
    }
}
