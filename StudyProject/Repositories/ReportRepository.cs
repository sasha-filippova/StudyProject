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
        /// <param name="context">Контекст базы данных.</param>
        public ReportRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все отчеты асинхронно.
        /// </summary>
        /// <returns>Список отчетов.</returns>
        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        /// <summary>
        /// Добавить новый отчет асинхронно.
        /// </summary>
        /// <param name="report">Новый отчет</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный отчет</returns>
        public async Task<Report> AddReportAsync(Report report, CancellationToken cancellationToken)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync(cancellationToken);
            return report;
        }
        /// <summary>
        /// Получить отчет по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденный отчет</returns>
        public async Task<Report> GetReportByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Reports.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Обновить существующий отчет асинхронно.
        /// </summary>
        /// <param name="updatedReport">Обновленный отчет</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленный отчет</returns>
        public async Task<Report> UpdateReportAsync(Report updatedReport, CancellationToken cancellationToken)
        {
            _context.Entry(updatedReport).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedReport;
        }
        /// <summary>
        /// Удалить отчет по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленный отчет</returns>
        public async Task<Report> DeleteReportAsync(int id, CancellationToken cancellationToken)
        {
            var reportToDelete = await _context.Reports.FindAsync(id, cancellationToken);
            if (reportToDelete != null)
            {
                _context.Reports.Remove(reportToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return reportToDelete;

        }
    }
}
