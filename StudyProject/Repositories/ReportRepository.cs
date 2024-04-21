using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class ReportRepository
    {
        private readonly ProjectManagementContext _context;

        public ReportRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> AddReportsAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> GetReportsById(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<Report> UpdateReports(Report updatedReport)
        {
            _context.Entry(updatedReport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedReport;
        }
        public async Task<Report> DeleteReports(int id)
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
