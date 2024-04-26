using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления задачами в базе данных.
    /// </summary>
    public class TaskRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для TaskRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public TaskRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все задачи асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список задач</returns>
        public async Task<List<Models.Task>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            return await _context.Tasks.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить новую задачу асинхронно.
        /// </summary>
        /// <param name="task">Новая задача</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленная задача</returns>
        public async Task<Models.Task> AddTaskAsync(Models.Task task, CancellationToken cancellationToken)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }
        /// <summary>
        /// Получить задачу по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденная задача</returns>
        public async Task<Models.Task> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Tasks.FindAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновить существующую задачу асинхронно.
        /// </summary>
        /// <param name="updatedTask">Обновленная задача</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленная задача</returns>
        public async Task<Models.Task> UpdateTaskAsync(Models.Task updatedTask, CancellationToken cancellationToken)
        {
            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedTask;
        }
        /// <summary>
        /// Удалить задачу по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленная задача</returns>
        public async Task<Models.Task> DeleteTaskAsync(int id, CancellationToken cancellationToken)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id, cancellationToken);
            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return taskToDelete;

        }
    }
}
