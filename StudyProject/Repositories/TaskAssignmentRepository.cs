using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления назначениями задачи в базе данных.
    /// </summary>
    public class TaskAssignmentRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для TaskAssignmentRepository.
        /// </summary>
        /// <param name="context"></param>
        public TaskAssignmentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все назначения задач асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TaskAssignment>> GetAllTaskAssignmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.TaskAssignments.ToListAsync();
        }

        /// <summary>
        /// Добавить новое назначение асинхронно.
        /// </summary>
        /// <param name="student"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TaskAssignment> AddTaskAssignmentsAsync(TaskAssignment student, CancellationToken cancellationToken)
        {
            _context.TaskAssignments.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        /// <summary>
        /// Получить назначение по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TaskAssignment> GetTaskAssignmentsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.TaskAssignments.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующее назначение асинхронно.
        /// </summary>
        /// <param name="updatedTaskAssignment"></param>
        /// <returns></returns>
        public async Task<TaskAssignment> UpdateTaskAssignmentsAsync(TaskAssignment updatedTaskAssignment)
        {
            _context.Entry(updatedTaskAssignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedTaskAssignment;
        }
        /// <summary>
        /// Удалить назначение по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TaskAssignment> DeleteTaskAssignmentsAsync(int id, CancellationToken cancellationToken)
        {
            var taskAssignmentToDelete = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignmentToDelete != null)
            {
                _context.TaskAssignments.Remove(taskAssignmentToDelete);
                await _context.SaveChangesAsync();
            }
            return taskAssignmentToDelete;

        }
    }
}
