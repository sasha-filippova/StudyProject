using Microsoft.EntityFrameworkCore;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class CommentRepository
    {
        private readonly ProjectManagementContext _context;

        public CommentRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        public async Task<Comment> AddCommentsAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetCommentsById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment> UpdateComments(Comment updatedComments)
        {
            _context.Entry(updatedComments).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedComments;
        }
        public async Task<Comment> DeleteComments(int id)
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
