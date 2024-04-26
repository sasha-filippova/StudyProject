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
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список категорий.</returns>
        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Добавить новую категорию асинхронно.
        /// </summary>
        /// <param name="category">Новая категория для добавления.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленная категория.</returns>
        public async Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        /// <summary>
        /// Получить категорию по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Найденная категория.</returns>
        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Обновить существующую категорию асинхронно.
        /// </summary>
        /// <param name="updatedCategories">Обновленная информация о категории.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Обновленная категория.</returns>
        public async Task<Category> UpdateCategoryAsync(Category updatedCategories, CancellationToken cancellationToken)
        {
            _context.Entry(updatedCategories).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return updatedCategories;
        }

        /// <summary>
        /// Удалить категорию по ID асинхронно.
        /// </summary>
        /// <param name="id">Идентификатор категории для удаления.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Удаленная категория.</returns>
        public async Task<Category> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id, cancellationToken);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return categoryToDelete;

        }

    }
}
