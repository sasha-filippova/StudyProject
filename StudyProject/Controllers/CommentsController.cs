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
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список комментариев.</returns>
        [HttpGet]
        public async Task<List<Comment>> GetAllComment(CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAllCommentsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить комментарий по ID.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Комментарий по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id, CancellationToken cancellationToken)
        {
            var _comment = await _commentRepository.GetCommentByIdAsync(id, cancellationToken);
            if (_comment == null)
            {
                return NotFound();
            }
            return _comment;
        }

        /// <summary>
        /// Добавить новый комментарий.
        /// </summary>
        /// <param name="newComment">Новый комментарий.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный комментарий.</returns>
        [HttpPost]
        public async Task<ActionResult<Comment>> AddCommentAsync(Comment newComment, CancellationToken cancellationToken)
        {
            await _commentRepository.AddCommentAsync(newComment, cancellationToken);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment);
        }

        /// <summary>
        /// Обновить существующий комментарий по ID.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="updatedComment">Обновленный комментарий.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment updatedComment, CancellationToken cancellationToken)
        {
            if (id != updatedComment.Id)
            {
                return BadRequest();
            }
            await _commentRepository.UpdateCommentAsync(updatedComment, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить комментарий по ID.
        /// </summary>
        /// <param name="id">Идентификатор комментарий.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken cancellationToken)
        {
            var commentToDelete = await _commentRepository.GetCommentByIdAsync(id, cancellationToken);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _commentRepository.DeleteCommentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
