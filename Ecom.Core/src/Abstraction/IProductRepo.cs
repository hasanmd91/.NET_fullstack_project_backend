using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IProductRepo
    {

        Product CreateOne(Product product);
        IEnumerable<Product> GetAll(GetAllParams options);
        Product GetOne(Guid id);
        Product UpdateOne(Guid id, Product product);
        bool DeleteOne(Guid id);
    }
}