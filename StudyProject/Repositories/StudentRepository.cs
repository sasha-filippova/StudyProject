using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления студентами в базе данных.
    /// </summary>
    public class StudentRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для StudentRepository.
        /// </summary>
        /// <param name="context"></param>
        public StudentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить всех студентов асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Students.ToListAsync();
        }
        /// <summary>
        /// Добавить нового студента асинхронно.
        /// </summary>
        /// <param name="student"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Student> AddStudentsAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        /// <summary>
        /// Получить студента по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Student> GetStudentsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Students.FindAsync(id);
        }

        /// <summary>
        /// Обновить существующего студента асинхронно.
        /// </summary>
        /// <param name="updatedStudent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Student> UpdateStudentsAsync(Student updatedStudent, CancellationToken cancellationToken)
        {
            _context.Entry(updatedStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedStudent;
        }
        /// <summary>
        /// Удалить студента по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Student> DeleteStudentsAsync(int id, CancellationToken cancellationToken)
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
