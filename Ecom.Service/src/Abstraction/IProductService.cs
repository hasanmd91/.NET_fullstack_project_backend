using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IProductService
    {
        Task<ProductReadDTO> CreateOneProductAsync(ProductCreateDTO productCreateDTO);
        Task<IEnumerable<ProductReadDTO>> GetAllProductAsync(GetAllParams options);
        Task<ProductReadDTO> GetOneProductByIdAsync(Guid id);
        Task<ProductReadDTO> UpdateOneProductAsync(Guid id, ProductUpdateDTO product);
        Task<bool> DeleteOneProductAsync(Guid id);
    }
}