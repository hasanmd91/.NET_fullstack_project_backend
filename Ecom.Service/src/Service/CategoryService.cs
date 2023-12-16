using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

namespace Ecom.Service.src.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryReadDTO>> GetAllCategoryAsync(GetAllParams options)
        {
            var result = await _categoryRepo.GetAllCategoryAsync(options);
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(result);
        }


        public async Task<CategoryReadDTO> GetOneCategoryByIdAsync(Guid categoryId)
        {
            var result = await _categoryRepo.GetOneCategoryByIdAsync(categoryId) ?? throw CustomException.NotFoundException();
            return _mapper.Map<Category, CategoryReadDTO>(result);
        }


        public async Task<CategoryReadDTO> CreateOneCategoryAsync(CategoryCreateDTO category)
        {
            var newCategory = _mapper.Map<CategoryCreateDTO, Category>(category);
            var result = await _categoryRepo.CreateOneCategoryAsync(newCategory) ?? throw CustomException.BadRequestException();
            return _mapper.Map<Category, CategoryReadDTO>(result);
        }


        public async Task<bool> DeleteOneByIdCategoryAsync(Guid categoryId)
        {
            var deleteResult = await _categoryRepo.DeleteOneByIdCategoryAsync(categoryId);
            if (!deleteResult)
            {
                throw CustomException.NotFoundException();
            }
            return deleteResult;
        }

        public async Task<CategoryReadDTO> UpdateOneCategoryAsync(Guid categoryId, CategoryUpdateDTO category)
        {
            var updatedCategory = _mapper.Map<CategoryUpdateDTO, Category>(category);
            var result = await _categoryRepo.UpdateOneCategoryAsync(categoryId, updatedCategory) ?? throw CustomException.BadRequestException();
            return _mapper.Map<Category, CategoryReadDTO>(result);
        }
    }
}