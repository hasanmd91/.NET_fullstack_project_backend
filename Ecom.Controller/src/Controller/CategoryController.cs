using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAllCategoryAsync([FromQuery] GetAllParams options)
        {
            var result = await _categoryService.GetAllCategoryAsync(options);
            return result.ToArray();
        }


        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryReadDTO>> GetOneCategoryByIdAsync(Guid categoryId)
        {
            return Ok(await _categoryService.GetOneCategoryByIdAsync(categoryId));
        }


        [HttpPost()]
        public async Task<ActionResult<CategoryReadDTO>> CreateOneCategoryAsync([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            return Ok(await _categoryService.CreateOneCategoryAsync(categoryCreateDTO));
        }



        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteOneByIdCategoryAsync(Guid categoryId)
        {
            return StatusCode(204, await _categoryService.DeleteOneByIdCategoryAsync(categoryId));
        }



        [HttpPatch("{categoryId}")]
        public async Task<ActionResult<CategoryReadDTO>> UpdateOneCategoryAsync(Guid categoryId, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            return Ok(await _categoryService.UpdateOneCategoryAsync(categoryId, categoryUpdateDTO));
        }

    }
}