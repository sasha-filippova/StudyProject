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
        /// <param name="context"></param>
        public StatusRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все статусы асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Status>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            return await _context.Statuses.ToListAsync();
        }
        /// <summary>
        /// Добавить новый статус асинхронно.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Status> AddStatusesAsync(Status status, CancellationToken cancellationToken)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return status;
        }
        /// <summary>
        /// Получить статус по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Status> GetStatusesByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Statuses.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующий статус асинхронно.
        /// </summary>
        /// <param name="updatedStatus"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Status> UpdateStatusesAsync(Status updatedStatus, CancellationToken cancellationToken)
        {
            _context.Entry(updatedStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedStatus;
        }
        /// <summary>
        /// Удалить статус по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Status> DeleteStatusesAsync(int id, CancellationToken cancellationToken)
        {
            var statusToDelete = await _context.Statuses.FindAsync(id);
            if (statusToDelete != null)
            {
                _context.Statuses.Remove(statusToDelete);
                await _context.SaveChangesAsync();
            }
            return statusToDelete;

        }
    }
}
