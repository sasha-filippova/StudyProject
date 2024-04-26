using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StudyProject.Models;
using System.Threading;

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
        /// <param name="context">Контекст базы данных.</param>
        public MemberRepository(ProjectManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить всех участников проектов асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список участников.</returns>
        public async Task<List<Member>> GetAllMemberssAsync(CancellationToken cancellationToken)
        {
            return await _context.Members.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Добавить нового участника асинхронно.
        /// </summary>
        /// <param name="member">Новый участник.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный участник.</returns>
        public async Task<Member> AddMemberAsync(Member member, CancellationToken cancellationToken)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync(cancellationToken);
            return member;
        }

        /// <summary>
        /// Получить участника по ID асинхронно.
        /// </summary>
        /// <param name="studentId">Идентификатор студента.</param>
        /// <param name="projectId">Идентификатор проекта.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Найденный участник.</returns>
        public async Task<Member> GetMemberByIdAsync(int studentId, int projectId, CancellationToken cancellationToken)
        {
            return await _context.Members.FindAsync(studentId, projectId, cancellationToken);
        }

        /// <summary>
        /// Обновить существующего участника асинхронно.
        /// </summary>
        /// <param name="updatedMembers">Обновленный участник.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Обновленный участник.</returns>
        public async Task<Member> UpdateMemberAsync(Member updatedMembers, CancellationToken cancellationToken)
        {
            _context.Entry(updatedMembers).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedMembers;
        }

        /// <summary>
        /// Удалить участника по ID асинхронно.
        /// </summary>
        /// <param name="studentId">Идентификатор студента.</param>
        /// <param name="projectId">Идентификатор проекта.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Удаленный участник.</returns>
        public async Task<Member> DeleteMemberAsync(int studentId, int projectId, CancellationToken cancellationToken)
        {
            var memberToDelete = await _context.Members.FindAsync(studentId, projectId);
            if (memberToDelete != null)
            {
                _context.Members.Remove(memberToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return memberToDelete;

        }
    }
}
