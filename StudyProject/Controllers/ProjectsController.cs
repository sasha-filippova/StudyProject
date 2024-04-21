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
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        private readonly ProjectRepository _projectRepository;

        
        public ProjectsController(ProjectRepository projectRepository)
        {
            //_context = context;
            _projectRepository = projectRepository;
        }

        
        [HttpGet]
        public async Task<List<Project>> GetAllProject()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectsById(int id)
        {
            var _project = await _projectRepository.GetProjectById(id);
            if (_project == null)
            {
                return NotFound();
            }
            return _project;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddProjectsAsync(Project newProject)
        {
            await _projectRepository.AddProjectAsync(newProject);
            return CreatedAtAction(nameof(GetProjectsById), new { id = newProject.ProjectId }, newProject);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project updatedProject)
        {
            if (id != updatedProject.ProjectId)
            {
                return BadRequest();
            }
            await _projectRepository.UpdateProjects(updatedProject);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var commentToDelete = await _projectRepository.GetProjectById(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _projectRepository.DeleteProjects(id);
            return NoContent();
        }
    }
}
