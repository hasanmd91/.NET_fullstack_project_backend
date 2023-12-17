using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface ICategoryService
    {
        Task<CategoryReadDTO> CreateOneCategoryAsync(CategoryCreateDTO categoryCreateDTO);
        Task<IEnumerable<CategoryReadDTO>> GetAllCategoryAsync(GetAllParams options);
        Task<CategoryReadDTO> GetOneCategoryByIdAsync(Guid categoryId);
        Task<CategoryReadDTO> UpdateOneCategoryAsync(Guid categoryId, CategoryUpdateDTO categoryUpdateDTO);
        Task<bool> DeleteOneByIdCategoryAsync(Guid categoryId);
    }
}