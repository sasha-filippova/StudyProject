using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class StatusRepository
    {
        private readonly ProjectManagementContext _context;

        public StatusRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Status>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> AddStatusesAsync(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<Status> GetStatusesById(int id)
        {
            return await _context.Statuses.FindAsync(id);
        }

        public async Task<Status> UpdateStatuses(Status updatedStatus)
        {
            _context.Entry(updatedStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedStatus;
        }
        public async Task<Status> DeleteStatuses(int id)
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
