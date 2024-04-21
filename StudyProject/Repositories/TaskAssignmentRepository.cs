using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class TaskAssignmentRepository
    {
        private readonly ProjectManagementContext _context;

        public TaskAssignmentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<TaskAssignment>> GetAllTaskAssignmentsAsync()
        {
            return await _context.TaskAssignments.ToListAsync();
        }

        public async Task<TaskAssignment> AddTaskAssignmentsAsync(TaskAssignment student)
        {
            _context.TaskAssignments.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<TaskAssignment> GetTaskAssignmentsById(int id)
        {
            return await _context.TaskAssignments.FindAsync(id);
        }

        public async Task<TaskAssignment> UpdateTaskAssignments(TaskAssignment updatedTaskAssignment)
        {
            _context.Entry(updatedTaskAssignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedTaskAssignment;
        }
        public async Task<TaskAssignment> DeleteTaskAssignments(int id)
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
