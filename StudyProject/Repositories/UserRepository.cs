using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления пользователями в базе данных.
    /// </summary>
    public class UserRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для UserRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public UserRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить всех пользователей асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список пользователей</returns>
        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Добавить нового пользователя асинхронно.
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленный пользователь</returns>
        public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
        /// <summary>
        /// Получить пользователя по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденный пользователь</returns>
        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Обновить существующего пользователя асинхронно.
        /// </summary>
        /// <param name="updatedUser">Обновленный пользователь</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленный пользователь</returns>
        public async Task<User> UpdateUserAsync(User updatedUser, CancellationToken cancellationToken)
        {
            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedUser;
        }
        /// <summary>
        /// Удалить пользователя по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленный пользователь</returns>
        public async Task<User> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var userToDelete = await _context.Users.FindAsync(id, cancellationToken);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return userToDelete;

        }
    }
}
