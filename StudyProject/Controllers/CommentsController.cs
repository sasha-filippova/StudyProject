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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

       
        private readonly CommentRepository _commentRepository;

       
        public CommentsController(CommentRepository commentRepository)
        {
            //_context = context;
            _commentRepository = commentRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Comment>> GetAllComment()
        {
            return await _commentRepository.GetAllCommentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var _comment = await _commentRepository.GetCommentsById(id);
            if (_comment == null)
            {
                return NotFound();
            }
            return _comment;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddCommentAsync(Comment newComment)
        {
            await _commentRepository.AddCommentsAsync(newComment);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.CommentId }, newComment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment updatedComment)
        {
            if (id != updatedComment.CommentId)
            {
                return BadRequest();
            }
            await _commentRepository.UpdateComments(updatedComment);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var commentToDelete = await _commentRepository.GetCommentsById(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            await _commentRepository.DeleteComments(id);
            return NoContent();
        }
    }
}
