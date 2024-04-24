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
    /// Контроллер для управления участниками проектов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly MemberRepository _memberRepository;

        /// <summary>
        /// Конструктор для MembersController.
        /// </summary>
        /// <param name="memberRepository">Репозиторий участников.</param>
        public MembersController(MemberRepository memberRepository)
        {
        
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Получить всех учатсников.
        /// </summary>
        [HttpGet]
        public async Task<List<Member>> GetAllMember(CancellationToken cancellationToken)
        {
            return await _memberRepository.GetAllMemberssAsync(cancellationToken);
        }

        /// <summary>
        /// Получить участника по ID.
        /// </summary>
        [HttpGet("{studentId}/{projectId}")]
        public async Task<ActionResult<Member>> GetMemberByIds(int studentId, int projectId, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetMembersByIdAsync(studentId, projectId, cancellationToken);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        /// <summary>
        /// Добавить нового участника.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member newMember, CancellationToken cancellationToken)
        {
            await _memberRepository.AddMembersAsync(newMember, cancellationToken);
            return CreatedAtAction(nameof(GetMemberByIds), new { studentId = newMember.StudentId, projectId = newMember.ProjectId }, newMember);
        }


        /// <summary>
        /// Удалить участника по ID.
        /// </summary>
        [HttpDelete("{studentId}/{projectId}")]
        public async Task<IActionResult> DeleteMember(int studentId, int projectId, CancellationToken cancellationToken)
        {
            var memberToDelete = await _memberRepository.GetMembersByIdAsync(studentId, projectId, cancellationToken);
            if (memberToDelete == null)
            {
                return NotFound();
            }
            await _memberRepository.DeleteMembersAsync(studentId, projectId, cancellationToken);
            return NoContent();
        }

        
    }
}
