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
    public class MembersController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly MemberRepository _memberRepository;

        //public EController(IEventRepository eventRepository)
        //{
        //    _eventRepository = eventRepository;
        //}
        public MembersController(MemberRepository memberRepository)
        {
            //_context = context;
            _memberRepository = memberRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Member>> GetAllMember()
        {
            return await _memberRepository.GetAllMemberssAsync();
        }

        [HttpGet("{studentId}/{projectId}")]
        public async Task<ActionResult<Member>> GetMemberByIds(int studentId, int projectId)
        {
            var member = await _memberRepository.GetMembersById(studentId, projectId);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member newMember)
        {
            await _memberRepository.AddMembersAsync(newMember);
            return CreatedAtAction(nameof(GetMemberByIds), new { studentId = newMember.StudentId, projectId = newMember.ProjectId }, newMember);
        }

        //[HttpPut("{studentId}/{projectId}")]
        //public async Task<IActionResult> UpdateMember(int studentId, int projectId, Member updatedMember)
        //{
        //    if (studentId != updatedMember.StudentId || projectId != updatedMember.ProjectId)
        //    {
        //        return BadRequest();
        //    }
        //    await _memberRepository.UpdateMembers(updatedMember);
        //    return NoContent();
        //}

        [HttpDelete("{studentId}/{projectId}")]
        public async Task<IActionResult> DeleteMember(int studentId, int projectId)
        {
            var memberToDelete = await _memberRepository.GetMembersById(studentId, projectId);
            if (memberToDelete == null)
            {
                return NotFound();
            }
            await _memberRepository.DeleteMembers(studentId, projectId);
            return NoContent();
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Member>> GetMembersById(int id)
        //{
        //    var _member = await _memberRepository.GetMembersById(id);
        //    if (_member == null)
        //    {
        //        return NotFound();
        //    }
        //    return _member;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Member>> AddMembersAsync(Member newMember)
        //{
        //    await _memberRepository.AddMembersAsync(newMember);
        //    return CreatedAtAction(nameof(GetMembersById), new { id = newMember.Id }, newMember);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMembers(int id, Member updatedComment)
        //{
        //    if (id != updatedComment.CommentId)
        //    {
        //        return BadRequest();
        //    }
        //    await _memberRepository.UpdateComments(updatedComment);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMembers(int id)
        //{
        //    var commentToDelete = await _memberRepository.GetMembersById(id);
        //    if (commentToDelete == null)
        //    {
        //        return NotFound();
        //    }
        //    await _memberRepository.DeleteComments(id);
        //    return NoContent();
        //}
    }
}
