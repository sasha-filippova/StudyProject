using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudyProject.Models;

namespace StudyProject.Repositories
{
    public class CategoryRepository
    {
        private readonly ProjectManagementContext _context;

        public CategoryRepository(ProjectManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> AddCategoriesAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoriesById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategories(Category updatedCategories)
        {
            _context.Entry(updatedCategories).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedCategories;
        }
        public async Task<Category> DeleteCategories(int id)
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
