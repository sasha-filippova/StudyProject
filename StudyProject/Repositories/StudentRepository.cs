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
        /// <param name="context">Контекст базы данных.</param>
        public StudentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить всех студентов асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список студентов</returns>
        public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Students.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить нового студента асинхронно.
        /// </summary>
        /// <param name="student">Новый студент</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленный студент</returns>
        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student;
        }
        /// <summary>
        /// Получить студента по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор студента</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденный студент</returns>
        public async Task<Student> GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Students.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Обновить существующего студента асинхронно.
        /// </summary>
        /// <param name="updatedStudent">ОБновленный студент</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленный студент</returns>
        public async Task<Student> UpdateStudentAsync(Student updatedStudent, CancellationToken cancellationToken)
        {
            _context.Entry(updatedStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedStudent;
        }
        /// <summary>
        /// Удалить студента по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор студента</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленный студент</returns>
        public async Task<Student> DeleteStudentAsync(int id, CancellationToken cancellationToken)
        {
            var statusToDelete = await _context.Students.FindAsync(id, cancellationToken);
            if (statusToDelete != null)
            {
                _context.Students.Remove(statusToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return statusToDelete;

        }
    }
}
