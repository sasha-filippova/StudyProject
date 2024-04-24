using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyProject;
using StudyProject.Models;
using StudyProject.Repositories;

namespace StudyProject.Controllers
{
    /// <summary>
    /// Контроллер для управления комментариями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

       
        private readonly CommentRepository _commentRepository;

        /// <summary>
        /// Конструктор для CommentsController.
        /// </summary>
        /// <param name="commentRepository">Репозиторий комментариев.</param>
        public CommentsController(CommentRepository commentRepository)
        {
            
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Получить все комментарии.
        /// </summary>
        [HttpGet]
        public async Task<List<Comment>> GetAllComment(CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAllCommentsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить комментарий по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id, CancellationToken cancellationToken)
        {
            var _comment = await _commentRepository.GetCommentsByIdAsync(id, cancellationToken);
            if (_comment == null)
            {
                return NotFound();
            }
            return _comment;
        }

        /// <summary>
        /// Добавить новый комментарий.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Comment>> AddCommentAsync(Comment newComment, CancellationToken cancellationToken)
        {
            await _commentRepository.AddCommentsAsync(newComment, cancellationToken);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.CommentId }, newComment);
        }

        /// <summary>
        /// Обновить существующий комментарий по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment updatedComment, CancellationToken cancellationToken)
        {
            if (id != updatedComment.CommentId)
            {
                return BadRequest();
            }
            await _commentRepository.UpdateCommentsAsync(updatedComment, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить комментарий по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _commentRepository.GetCommentsByIdAsync(id, cancellationToken);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _commentRepository.DeleteCommentsAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
