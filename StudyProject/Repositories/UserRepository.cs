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
        /// <param name="context"></param>
        public UserRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить всех пользователей асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Добавить нового пользователя асинхронно.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> AddUsersAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        /// <summary>
        /// Получить пользователя по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> GetUsersByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Обновить существующего пользователя асинхронно.
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> UpdateUsersAsync(User updatedUser, CancellationToken cancellationToken)
        {
            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedUser;
        }
        /// <summary>
        /// Удалить пользователя по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> DeleteUsersAsync(int id, CancellationToken cancellationToken)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            return userToDelete;

        }
    }
}
