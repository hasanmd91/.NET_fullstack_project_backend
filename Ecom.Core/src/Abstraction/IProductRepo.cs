using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IProductRepo
    {

        Product CreateOneProductAsync(Product product);
        IEnumerable<Product> GetAllProductAsync(GetAllParams options);
        Product GetOneProductByIdAsync(Guid id);
        Product UpdateOneProductAsync(Guid id, Product product);
        bool DeleteOneProductAsync(Guid id);
    }
}