using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudyProject;
using StudyProject.Models;
using StudyProject.Repositories;

namespace StudyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly CategoryRepository _categoryRepository;

        //public EController(IEventRepository eventRepository)
        //{
        //    _eventRepository = eventRepository;
        //}
        public CategoriesController(CategoryRepository categoryRepository)
        {
            //_context = context;
            _categoryRepository = categoryRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<List<Category>> GetAllCategory()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var _category = await _categoryRepository.GetCategoriesById(id);
            if (_category == null)
            {
                return NotFound();
            }
            return _category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category newCategory)
        {
            await _categoryRepository.AddCategoriesAsync(newCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
            {
                return BadRequest();
            }
            await _categoryRepository.UpdateCategories(updatedCategory);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryToDelete = await _categoryRepository.GetCategoriesById(id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategories(id);
            return NoContent();
        }

    }
}
