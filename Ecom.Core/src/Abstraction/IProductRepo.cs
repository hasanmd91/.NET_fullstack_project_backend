using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IProductRepo
    {

        Task<Product> CreateOneProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync(GetAllParams options);
        Task<Product> GetOneProductByIdAsync(Guid id);
        Task<Product> UpdateOneProductAsync(Product product);
        Task<bool> DeleteOneProductAsync(Guid id);
    }
}