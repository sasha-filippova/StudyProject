using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class RoleRepository
    {
        private readonly ProjectManagementContext _context;

        public RoleRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> AddRolesAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> GetRolesById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> UpdateRoles(Role updatedRole)
        {
            _context.Entry(updatedRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedRole;
        }
        public async Task<Role> DeleteRoles(int id)
        {
            var roleToDelete = await _context.Roles.FindAsync(id);
            if (roleToDelete != null)
            {
                _context.Roles.Remove(roleToDelete);
                await _context.SaveChangesAsync();
            }
            return roleToDelete;

        }
    }
}
