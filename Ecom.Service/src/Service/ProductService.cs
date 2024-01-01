using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

namespace Ecom.Service.src.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo, ICategoryRepo category, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _categoryRepo = category;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllProductAsync(GetAllParams options)
        {
            var result = await _productRepo.GetAllProductAsync(options);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(result);
        }

        public async Task<ProductReadDTO> CreateOneProductAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<ProductCreateDTO, Product>(productCreateDTO);
            var result = await _productRepo.CreateOneProductAsync(product) ?? throw CustomException.BadRequestException();
            return _mapper.Map<Product, ProductReadDTO>(result);
        }

        public async Task<bool> DeleteOneProductAsync(Guid productId)
        {
            var result = await _productRepo.DeleteOneProductAsync(productId);

            if (!result)
            {
                throw CustomException.NotFoundException();
            }
            return result;
        }


        public async Task<ProductReadDTO> GetOneProductByIdAsync(Guid productId)
        {
            var result = await _productRepo.GetOneProductByIdAsync(productId) ?? throw CustomException.NotFoundException("Product not found");
            return _mapper.Map<Product, ProductReadDTO>(result);

        }
        public async Task<ProductReadDTO> UpdateOneProductAsync(Guid productId, ProductUpdateDTO productUpdateDTO)
        {
            var productToUpdate = await _productRepo.GetOneProductByIdAsync(productId) ?? throw CustomException.NotFoundException("Product not found");

            if (productUpdateDTO.CategoryId is not null)
            {
                var categoryId = (Guid)productUpdateDTO.CategoryId;
                var foundCategory = await _categoryRepo.GetOneCategoryByIdAsync(categoryId) ?? throw CustomException.NotFoundException("Category with this input Id is not found");
                var updatedProduct = _mapper.Map(productUpdateDTO, productToUpdate);
                updatedProduct.CategoryId = categoryId;

                var result = await _productRepo.UpdateOneProductAsync(updatedProduct);
                return _mapper.Map<Product, ProductReadDTO>(result);
            }
            else
            {
                var updatedProduct = _mapper.Map(productUpdateDTO, productToUpdate);
                var result = await _productRepo.UpdateOneProductAsync(updatedProduct);
                return _mapper.Map<Product, ProductReadDTO>(result);
            }
        }

    }
}