using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

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
        /// <param name="context"></param>
        public RoleRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все роли асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync();
        }
        /// <summary>
        /// Добавить новую роль асинхронно.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Role> AddRolesAsync(Role role, CancellationToken cancellationToken)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        /// <summary>
        /// Получить роль по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Role> GetRolesByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles.FindAsync(id);
        }
        /// <summary>
        /// Обновить существующую роль асинхронно.
        /// </summary>
        /// <param name="updatedRole"></param>
        /// <returns></returns>
        public async Task<Role> UpdateRolesAsync(Role updatedRole)
        {
            _context.Entry(updatedRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedRole;
        }
        /// <summary>
        /// Удалить роль по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Role> DeleteRolesAsync(int id, CancellationToken cancellationToken)
        {
            var roleToDelete = await _context.Roles.FindAsync(id);
            if (roleToDelete != null)
            {
                _context.Roles.Remove(roleToDelete);
                await _context.SaveChangesAsync();
            }
            return roleToDelete;

        }
        /// <summary>
        /// Асинхронно обновляет роль в базе данных.
        /// </summary>
        /// <param name="updatedRole"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal async System.Threading.Tasks.Task UpdateRolesAsync(Role updatedRole, object cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
