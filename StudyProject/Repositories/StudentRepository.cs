using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class StudentRepository
    {
        private readonly ProjectManagementContext _context;

        public StudentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> AddStudentsAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetStudentsById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> UpdateStudents(Student updatedStudent)
        {
            _context.Entry(updatedStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedStudent;
        }
        public async Task<Student> DeleteStudents(int id)
        {
            var statusToDelete = await _context.Students.FindAsync(id);
            if (statusToDelete != null)
            {
                _context.Students.Remove(statusToDelete);
                await _context.SaveChangesAsync();
            }
            return statusToDelete;

        }
    }
}
