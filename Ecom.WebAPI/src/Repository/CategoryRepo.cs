using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DbSet<Category> _category;
        private readonly DataBaseContext _database;
        public CategoryRepo(DataBaseContext dataBase)
        {
            _category = dataBase.Category;
            _database = dataBase;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync(GetAllParams options)
        {
            return await Task.Run(() =>
            {
                return _category
                    .Where(c => c.Name.Contains(options.Search))
                    .Skip(options.Offset)
                    .Take(options.Limit)
                    .ToListAsync();
            });
        }

        public async Task<Category> CreateOneCategoryAsync(Category category)
        {
            await _category.AddAsync(category);
            await _database.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> GetOneCategoryByIdAsync(Guid categoryId)
        {
            return await _category.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<bool> DeleteOneByIdCategoryAsync(Guid categoryId)
        {
            {
                var category = await _category.FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category != null)
                {
                    _category.Remove(category);
                    await _database.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<Category?> UpdateOneCategoryAsync(Category updatedCategory)
        {
            _database.Update(updatedCategory);
            await _database.SaveChangesAsync();
            return updatedCategory;
        }

    }
}