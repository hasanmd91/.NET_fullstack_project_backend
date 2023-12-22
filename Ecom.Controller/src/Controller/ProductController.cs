using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAllProductAsync([FromQuery] GetAllParams options)
        {
            var product = await _productService.GetAllProductAsync(options);
            return Ok(product);
        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductReadDTO>> GetOneProductByIdAsync(Guid productId)
        {
            var product = await _productService.GetOneProductByIdAsync(productId);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<ActionResult<ProductReadDTO>> CreateOneProductAsync([FromBody] ProductCreateDTO productCreateDTO)
        {
            var product = await _productService.CreateOneProductAsync(productCreateDTO);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<ActionResult<bool>> DeleteOneProductAsync(Guid productId)
        {
            var product = await _productService.DeleteOneProductAsync(productId);
            return StatusCode(204, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{productId}")]
        public async Task<ActionResult<ProductReadDTO>> UpdateOneProductAsync(Guid productId, ProductUpdateDTO productUpdateDTO)
        {
            var product = await _productService.UpdateOneProductAsync(productId, productUpdateDTO);
            return Ok(product);
        }

    }
}


