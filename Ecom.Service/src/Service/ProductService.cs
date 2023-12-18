using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDTO> CreateOneProductAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<ProductCreateDTO, Product>(productCreateDTO);
            var result = await _productRepo.CreateOneProductAsync(product);
            return _mapper.Map<Product, ProductReadDTO>(result);

        }
        public async Task<bool> DeleteOneProductAsync(Guid productId)
        {
            var result = await _productRepo.DeleteOneProductAsync(productId);
            return result;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllProductAsync(GetAllParams options)
        {
            var result = await _productRepo.GetAllProductAsync(options);
            Console.WriteLine("this is the service layer", result);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(result);
        }

        public async Task<ProductReadDTO> GetOneProductByIdAsync(Guid productId)
        {
            var result = await _productRepo.GetOneProductByIdAsync(productId);
            return _mapper.Map<Product, ProductReadDTO>(result);

        }

        public async Task<ProductReadDTO> UpdateOneProductAsync(Guid productId, ProductUpdateDTO productUpdateDTO)
        {
            var product = _mapper.Map<ProductUpdateDTO, Product>(productUpdateDTO);
            var result = await _productRepo.UpdateOneProductAsync(productId, product);
            return _mapper.Map<Product, ProductReadDTO>(result);

        }
    }
}