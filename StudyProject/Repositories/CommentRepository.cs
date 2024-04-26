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
        /// <param name="context">Контекст базы данных.</param>
        public CommentRepository(ProjectManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все комментарии асинхронно.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список комментариев.</returns>
        public async Task<List<Comment>> GetAllCommentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Comments.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Добавить новый комментарий асинхронно.
        /// </summary>
        /// <param name="comment">Новый комментарий.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный комментарий.</returns>
        public async Task<Comment> AddCommentAsync(Comment comment, CancellationToken cancellationToken)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return comment;
        }


        /// <summary>
        /// Получить комментарий по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Найденный комментарий.</returns>
        public async Task<Comment> GetCommentByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Comments.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Обновить существующий комментарий асинхронно.
        /// </summary>
        /// <param name="updatedComments">Обновленный комментарий.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Обновленный комментарий.</returns>
        public async Task<Comment> UpdateCommentAsync(Comment updatedComments, CancellationToken cancellationToken)
        {
            _context.Entry(updatedComments).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedComments;
        }

        /// <summary>
        /// Удалить комментарий по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Удаленный комментарий.</returns>
        public async Task<Comment> DeleteCommentAsync(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _context.Comments.FindAsync(id, cancellationToken);
            if (commentToDelete != null)
            {
                _context.Comments.Remove(commentToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return commentToDelete;

        }
    }
}
