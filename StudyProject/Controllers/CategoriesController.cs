using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    /// <summary>
    /// Контроллер для управления категориями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ProjectManagementContext _context;
        private readonly CategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор для CategoriesController.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public CategoriesController(CategoryRepository categoryRepository)
        {
            
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Получить все категории.
        /// </summary>
        
        [HttpGet]
        public async Task<List<Category>> GetAllCategory(CancellationToken cancellationToken = default)
        {
            return await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
        }

        /// <summary>
        /// Получить категорию по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id, CancellationToken cancellationToken = default)
        {
            var _category = await _categoryRepository.GetCategoriesByIdAsync(id, cancellationToken);
            if (_category == null)
            {
                return NotFound();
            }
            return _category;
        }

        /// <summary>
        /// Добавить новую категорию.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category newCategory, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.AddCategoriesAsync(newCategory, cancellationToken);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory);
        }

        /// <summary>
        /// Обновить существующую категорию по ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory, CancellationToken cancellationToken)
        {
            if (id != updatedCategory.CategoryId)
            {
                return BadRequest();
            }
            await _categoryRepository.UpdateCategoriesAsync(updatedCategory, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить категорию по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _categoryRepository.GetCategoriesByIdAsync(id, cancellationToken);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategoriesAsync(id, cancellationToken);
            return NoContent();
        }

    }
}
