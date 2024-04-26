using Microsoft.EntityFrameworkCore;
using StudyProject.Models;
using System.Threading;

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
        /// <param name="context">Контекст базы данных.</param>
        public TaskAssignmentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все назначения задач асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список назначений</returns>
        public async Task<List<TaskAssignment>> GetAllTaskAssignmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.TaskAssignments.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Добавить новое назначение асинхронно.
        /// </summary>
        /// <param name="student">Новое назначение студенту</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленное назначение</returns>
        public async Task<TaskAssignment> AddTaskAssignmentAsync(TaskAssignment student, CancellationToken cancellationToken)
        {
            _context.TaskAssignments.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student;
        }
        /// <summary>
        /// Получить назначение по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор назначения</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденное назначение</returns>
        public async Task<TaskAssignment> GetTaskAssignmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.TaskAssignments.FindAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновить существующее назначение асинхронно.
        /// </summary>
        /// <param name="updatedTaskAssignment">Обновленное назначение задачи</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленное назначение</returns>
        public async Task<TaskAssignment> UpdateTaskAssignmentAsync(TaskAssignment updatedTaskAssignment, CancellationToken cancellationToken)
        {
            _context.Entry(updatedTaskAssignment).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedTaskAssignment;
        }
        /// <summary>
        /// Удалить назначение по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор назначения</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленное назначение</returns>
        public async Task<TaskAssignment> DeleteTaskAssignmentAsync(int id, CancellationToken cancellationToken)
        {
            var taskAssignmentToDelete = await _context.TaskAssignments.FindAsync(id, cancellationToken);
            if (taskAssignmentToDelete != null)
            {
                _context.TaskAssignments.Remove(taskAssignmentToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return taskAssignmentToDelete;

        }
    }
}
