using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления статусами в базе данных.
    /// </summary>
    public class StatusRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для StatusRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public StatusRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все статусы асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список статусов</returns>
        public async Task<List<Status>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            return await _context.Statuses.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить новый статус асинхронно.
        /// </summary>
        /// <param name="status">Новый статус</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленный статус</returns>
        public async Task<Status> AddStatusAsync(Status status, CancellationToken cancellationToken)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync(cancellationToken);
            return status;
        }
        /// <summary>
        /// Получить статус по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор статуса</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденный статус</returns>
        public async Task<Status> GetStatusByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Statuses.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующий статус асинхронно.
        /// </summary>
        /// <param name="updatedStatus">Обновленный статус</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленный статус</returns>
        public async Task<Status> UpdateStatusAsync(Status updatedStatus, CancellationToken cancellationToken)
        {
            _context.Entry(updatedStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedStatus;
        }
        /// <summary>
        /// Удалить статус по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор статуса</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленный статус</returns>
        public async Task<Status> DeleteStatusAsync(int id, CancellationToken cancellationToken)
        {
            var statusToDelete = await _context.Statuses.FindAsync(id, cancellationToken);
            if (statusToDelete != null)
            {
                _context.Statuses.Remove(statusToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return statusToDelete;

        }
    }
}
