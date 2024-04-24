using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления проектами в базе данных.
    /// </summary>
    public class ProjectRepository
    {
        private readonly ProjectManagementContext _context;

        /// <summary>
        /// Конструктор для ProjectRepository.
        /// </summary>
        /// <param name="context"></param>
        public ProjectRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все проекты асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            return await _context.Projects.ToListAsync();
        }
        /// <summary>
        /// Добавить новый проект асинхронно.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Project> AddProjectAsync(Project project, CancellationToken cancellationToken)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
        /// <summary>
        /// Получить проект по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Project> GetProjectByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Projects.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующий проект асинхронно.
        /// </summary>
        /// <param name="updatedProject"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Project> UpdateProjectsAsync(Project updatedProject, CancellationToken cancellationToken)
        {
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedProject;
        }
        /// <summary>
        /// Удалить проект по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Project> DeleteProjectsAsync(int id, CancellationToken cancellationToken)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete != null)
            {
                _context.Projects.Remove(projectToDelete);
                await _context.SaveChangesAsync();
            }
            return projectToDelete;

        }
    }
}
