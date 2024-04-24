using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления комментариями в базе данных.
    /// </summary>
    public class CommentRepository
    {
        private readonly ProjectManagementContext _context;

        /// <summary>
        /// Конструктор для CommentRepository.
        /// </summary>
        /// <param name="context"></param>
        public CommentRepository(ProjectManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все комментарии асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetAllCommentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Comments.ToListAsync();
        }

        /// <summary>
        /// Добавить новый комментарий асинхронно.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Comment> AddCommentsAsync(Comment comment, CancellationToken cancellationToken)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        /// <summary>
        /// Получить комментарий по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Comment> GetCommentsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Comments.FindAsync(id);
        }

        /// <summary>
        /// Обновить существующий комментарий асинхронно.
        /// </summary>
        /// <param name="updatedComments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Comment> UpdateCommentsAsync(Comment updatedComments, CancellationToken cancellationToken)
        {
            _context.Entry(updatedComments).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedComments;
        }

        /// <summary>
        /// Удалить комментарий по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Comment> DeleteCommentsAsync(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _context.Comments.FindAsync(id);
            if (commentToDelete != null)
            {
                _context.Comments.Remove(commentToDelete);
                await _context.SaveChangesAsync();
            }
            return commentToDelete;

        }
    }
}
