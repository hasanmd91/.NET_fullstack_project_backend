using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.WebAPI.src.Repository
{
    public class ProductRepo : IProductRepo
    {
        public Product CreateOneProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOneProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProductAsync(GetAllParams options)
        {
            throw new NotImplementedException();
        }

        public Product GetOneProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product UpdateOneProductAsync(Guid id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}