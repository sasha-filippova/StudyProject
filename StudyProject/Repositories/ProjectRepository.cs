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
        /// <param name="context">Контекст базы данных.</param>
        public ProjectRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все проекты асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список проектов.</returns>
        public async Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            return await _context.Projects.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить новый проект асинхронно.
        /// </summary>
        /// <param name="project">Новый проект</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный проект.</returns>
        public async Task<Project> AddProjectAsync(Project project, CancellationToken cancellationToken)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);
            return project;
        }
        /// <summary>
        /// Получить проект по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор проекта.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Найденный проект.</returns>
        public async Task<Project> GetProjectByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Projects.FindAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновить существующий проект асинхронно.
        /// </summary>
        /// <param name="updatedProject">Обновленный проект.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Обновленный проект.</returns>
        public async Task<Project> UpdateProjectAsync(Project updatedProject, CancellationToken cancellationToken)
        {
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedProject;
        }
        /// <summary>
        /// Удалить проект по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Удаленный проект.</returns>
        public async Task<Project> DeleteProjectAsync(int id, CancellationToken cancellationToken)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete != null)
            {
                _context.Projects.Remove(projectToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return projectToDelete;

        }
    }
}
