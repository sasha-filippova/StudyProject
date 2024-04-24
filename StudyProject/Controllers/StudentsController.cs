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
    /// Контроллер для управления студентами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        private readonly StudentRepository _studentRepository;

        /// <summary>
        /// Конструктор для StudentsController.
        /// </summary>
        /// <param name="studentRepository">Репозиторий студентов.</param>
        public StudentsController(StudentRepository studentRepository)
        {
           
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Получить всех студентов.
        /// </summary>
       
        [HttpGet]
        public async Task<List<Student>> GetAllStudents(CancellationToken cancellationToken)
        {
            return await _studentRepository.GetAllStudentsAsync(cancellationToken);
        }

        /// <summary>
        /// Получить студента по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var _student = await _studentRepository.GetStudentsByIdAsync(id, cancellationToken);
            if (_student == null)
            {
                return NotFound();
            }
            return _student;
        }

        /// <summary>
        /// Добавить нового студента.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudentAsync(Student newStudent, CancellationToken cancellationToken)
        {
            await _studentRepository.AddStudentsAsync(newStudent, cancellationToken);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.StudentId }, newStudent);
        }

        /// <summary>
        /// Обновить информацию о существующем студенте по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent, CancellationToken cancellationToken)
        {
            if (id != updatedStudent.StudentId)
            {
                return BadRequest();
            }
            await _studentRepository.UpdateStudentsAsync(updatedStudent, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить студента по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            var statusToDelete = await _studentRepository.GetStudentsByIdAsync(id, cancellationToken);
            if (statusToDelete == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudentsAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
