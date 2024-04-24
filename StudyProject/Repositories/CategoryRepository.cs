using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    /// <summary>
    /// Репозиторий для управления категориями в базе данных.
    /// </summary>
    public class CategoryRepository
    {
        private readonly ProjectManagementContext _context;
        /// <summary>
        /// Конструктор для CategoryRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public CategoryRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все категории асинхронно.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Добавить новую категорию асинхронно.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Category> AddCategoriesAsync(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// Получить категорию по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Category> GetCategoriesByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.FindAsync(id);
        }

        /// <summary>
        /// Обновить существующую категорию асинхронно.
        /// </summary>
        /// <param name="updatedCategories"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Category> UpdateCategoriesAsync(Category updatedCategories, CancellationToken cancellationToken)
        {
            _context.Entry(updatedCategories).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedCategories;
        }

        /// <summary>
        /// Удалить категорию по ID асинхронно.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Category> DeleteCategoriesAsync(int id, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }
            return categoryToDelete;

        }

    }
}
