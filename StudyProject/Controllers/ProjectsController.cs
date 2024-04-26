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
    /// Контроллер для управления проектами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        private readonly ProjectRepository _projectRepository;

        /// <summary>
        /// Конструктор для ProjectsController.
        /// </summary>
        /// <param name="projectRepository">Репозиторий проектов.</param>
        public ProjectsController(ProjectRepository projectRepository)
        {
            
            _projectRepository = projectRepository;
        }

        /// <summary>
        ///  Получить все проекты.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список проектов.</returns>
        [HttpGet]
        public async Task<List<Project>> GetAllProject(CancellationToken cancellationToken)
        {
            return await _projectRepository.GetAllProjectsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить проект по ID.
        /// </summary>
        /// <param name="id">Идентификатор проекта.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Проект по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectsById(int id, CancellationToken cancellationToken)
        {
            var _project = await _projectRepository.GetProjectByIdAsync(id, cancellationToken);
            if (_project == null)
            {
                return NotFound();
            }
            return _project;
        }

        /// <summary>
        /// Добавить новый проект.
        /// </summary>
        /// <param name="newProject">Новый проект.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный проект</returns>
        [HttpPost]
        public async Task<ActionResult<Project>> AddProjectsAsync(Project newProject, CancellationToken cancellationToken)
        {
            await _projectRepository.AddProjectAsync(newProject, cancellationToken);
            return CreatedAtAction(nameof(GetProjectsById), new { id = newProject.Id }, newProject);
        }

        /// <summary>
        /// Обновить существующий проект по ID.
        /// </summary>
        /// <param name="id">Идентификатор проекта.</param>
        /// <param name="updatedProject">Обновленный проект.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project updatedProject, CancellationToken cancellationToken)
        {
            if (id != updatedProject.Id)
            {
                return BadRequest();
            }
            await _projectRepository.UpdateProjectAsync(updatedProject, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить проект по ID.
        /// </summary>
        /// <param name="id">Идентификатор проекта.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _projectRepository.GetProjectByIdAsync(id, cancellationToken);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _projectRepository.DeleteProjectAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
