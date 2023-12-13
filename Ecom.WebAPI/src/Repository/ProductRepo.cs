using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.WebAPI.src.Repository
{
    public class ProductRepo : IProductRepo
    {
        public Product CreateOne(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOneById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(GetAllParams options)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product UpdateOne(Guid id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}