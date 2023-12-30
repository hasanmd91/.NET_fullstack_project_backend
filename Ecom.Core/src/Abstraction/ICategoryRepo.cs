using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface ICategoryRepo
    {
        Task<Category> CreateOneCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoryAsync(GetAllParams options);
        Task<Category> GetOneCategoryByIdAsync(Guid categoryId);
        Task<Category> UpdateOneCategoryAsync(Category category);
        Task<bool> DeleteOneByIdCategoryAsync(Guid categoryId);
    }
}
