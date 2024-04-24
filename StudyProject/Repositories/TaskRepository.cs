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
        /// <param name="context"></param>
        public TaskRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все задачи асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Models.Task>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            return await _context.Tasks.ToListAsync();
        }
        /// <summary>
        /// Добавить новую задачу асинхронно.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.Task> AddTaskAsync(Models.Task task, CancellationToken cancellationToken)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        /// <summary>
        /// Получить задачу по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.Task> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Tasks.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующую задачу асинхронно.
        /// </summary>
        /// <param name="updatedTask"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.Task> UpdateTaskAsync(Models.Task updatedTask, CancellationToken cancellationToken)
        {
            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedTask;
        }
        /// <summary>
        /// Удалить задачу по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Models.Task> DeleteTaskAsync(int id, CancellationToken cancellationToken)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id);
            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }
            return taskToDelete;

        }
    }
}
