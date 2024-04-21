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
    public class StudentsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        //public StudentsController(ProjectManagementContext context)
        //{
        //    _context = context;
        //}

        private readonly StudentRepository _studentRepository;

        public StudentsController(StudentRepository studentRepository)
        {
            //_context = context;
            _studentRepository = studentRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var _student = await _studentRepository.GetStudentsById(id);
            if (_student == null)
            {
                return NotFound();
            }
            return _student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudentAsync(Student newStudent)
        {
            await _studentRepository.AddStudentsAsync(newStudent);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.StudentId }, newStudent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            if (id != updatedStudent.StudentId)
            {
                return BadRequest();
            }
            await _studentRepository.UpdateStudents(updatedStudent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var statusToDelete = await _studentRepository.GetStudentsById(id);
            if (statusToDelete == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudents(id);
            return NoContent();
        }
    }
}
