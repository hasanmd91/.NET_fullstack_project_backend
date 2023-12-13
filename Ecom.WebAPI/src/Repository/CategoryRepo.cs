using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.WebAPI.src.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        public Category CreateOne(Category Category)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOneById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll(GetAllParams options)
        {
            throw new NotImplementedException();
        }

        public Category GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public Category UpdateOne(Guid id, Category Category)
        {
            throw new NotImplementedException();
        }
    }
}