using Microsoft.EntityFrameworkCore;
using StudyProject.Models;
using System.Threading;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления ролями в базе данных.
    /// </summary>
    public class RoleRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для RoleRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public RoleRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все роли асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Список ролей</returns>
        public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить новую роль асинхронно.
        /// </summary>
        /// <param name="role">Новая роль</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Добавленная роль</returns>
        public async Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync(cancellationToken);
            return role;
        }
        /// <summary>
        /// Получить роль по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Найденная роль</returns>
        public async Task<Role> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles.FindAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновить существующую роль асинхронно.
        /// </summary>
        /// <param name="updatedRole">Обновленная роль</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Обновленная роль</returns>
        public async Task<Role> UpdateRoleAsync(Role updatedRole, CancellationToken cancellationToken)
        {
            _context.Entry(updatedRole).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedRole;
        }
        /// <summary>
        /// Удалить роль по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns>Удаленная роль</returns>
        public async Task<Role> DeleteRoleAsync(int id, CancellationToken cancellationToken)
        {
            var roleToDelete = await _context.Roles.FindAsync(id, cancellationToken);
            if (roleToDelete != null)
            {
                _context.Roles.Remove(roleToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return roleToDelete;

        }
        /// <summary>
        /// Асинхронно обновляет роль в базе данных.
        /// </summary>
        /// <param name="updatedRole">Обновленная роль</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <exception cref="NotImplementedException">Исключение.</exception>
        internal async System.Threading.Tasks.Task UpdateRoleAsync(Role updatedRole, object cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
