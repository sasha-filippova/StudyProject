using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления участниками проекта в базе данных.
    /// </summary>
    public class MemberRepository
    {
        private readonly ProjectManagementContext _context;

        /// <summary>
        /// Конструктор для MemberRepository.
        /// </summary>
        /// <param name="context"></param>
        public MemberRepository(ProjectManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить всех участников проектов асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Member>> GetAllMemberssAsync(CancellationToken cancellationToken)
        {
            return await _context.Members.ToListAsync();
        }

        /// <summary>
        /// Добавить нового участника асинхронно.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Member> AddMembersAsync(Member member, CancellationToken cancellationToken)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// Получить участника по ID асинхронно.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Member> GetMembersByIdAsync(int studentId, int projectId, CancellationToken cancellationToken)
        {
            return await _context.Members.FindAsync(studentId, projectId);
        }

        /// <summary>
        /// Обновить существующего участника асинхронно.
        /// </summary>
        /// <param name="updatedMembers"></param>
        /// <returns></returns>
        public async Task<Member> UpdateMembersAsync(Member updatedMembers)
        {
            _context.Entry(updatedMembers).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedMembers;
        }

        /// <summary>
        /// Удалить участника по ID асинхронно.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Member> DeleteMembersAsync(int studentId, int projectId, CancellationToken cancellationToken)
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
