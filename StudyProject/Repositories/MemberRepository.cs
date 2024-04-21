using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class MemberRepository
    {
        private readonly ProjectManagementContext _context;

        public MemberRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Member>> GetAllMemberssAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> AddMembersAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> GetMembersById(int studentId, int projectId)
        {
            return await _context.Members.FindAsync(studentId, projectId);
        }

        public async Task<Member> UpdateMembers(Member updatedMembers)
        {
            _context.Entry(updatedMembers).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedMembers;
        }
        public async Task<Member> DeleteMembers(int studentId, int projectId)
        {
            var memberToDelete = await _context.Members.FindAsync(studentId, projectId);
            if (memberToDelete != null)
            {
                _context.Members.Remove(memberToDelete);
                await _context.SaveChangesAsync();
            }
            return memberToDelete;

        }
    }
}
