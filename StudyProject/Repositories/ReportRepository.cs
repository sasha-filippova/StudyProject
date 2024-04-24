using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления отчетами в базе данных.
    /// </summary>
    public class ReportRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для ReportRepository.
        /// </summary>
        /// <param name="context"></param>
        public ReportRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все отчеты асинхронно.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        /// <summary>
        /// Добавить новый отчет асинхронно.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Report> AddReportsAsync(Report report, CancellationToken cancellationToken)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
        /// <summary>
        /// Получить отчет по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Report> GetReportsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Reports.FindAsync(id);
        }

        /// <summary>
        /// Обновить существующий отчет асинхронно.
        /// </summary>
        /// <param name="updatedReport"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Report> UpdateReportsAsync(Report updatedReport, CancellationToken cancellationToken)
        {
            _context.Entry(updatedReport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedReport;
        }
        /// <summary>
        /// Удалить отчет по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Report> DeleteReportsAsync(int id, CancellationToken cancellationToken)
        {
            var reportToDelete = await _context.Reports.FindAsync(id);
            if (reportToDelete != null)
            {
                _context.Reports.Remove(reportToDelete);
                await _context.SaveChangesAsync();
            }
            return reportToDelete;

        }
    }
}
