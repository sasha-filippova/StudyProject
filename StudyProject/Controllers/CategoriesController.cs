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
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        public async Task<List<Category>> GetAllCategory(CancellationToken cancellationToken = default)
        {
            return await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
        }

        /// <summary>
        /// Получить категорию по ID.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Категория по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id, CancellationToken cancellationToken = default)
        {
            var _category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (_category == null)
            {
                return NotFound();
            }
            return _category;
        }

        /// <summary>
        /// Добавить новую категорию.
        /// </summary>
        /// <param name="newCategory">Новая категория.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленная категория.</returns>
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category newCategory, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.AddCategoryAsync(newCategory, cancellationToken);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
        }

        /// <summary>
        /// Обновить существующую категорию по ID.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="updatedCategory">Обновленная категория.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory, CancellationToken cancellationToken)
        {
            if (id != updatedCategory.Id)
            {
                return BadRequest();
            }
            await _categoryRepository.UpdateCategoryAsync(updatedCategory, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить категорию по ID.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategoryAsync(id, cancellationToken);
            return NoContent();
        }

    }
}
