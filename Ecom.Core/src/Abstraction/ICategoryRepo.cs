using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface ICategoryRepo
    {

        Category CreateOne(Category Category);
        IEnumerable<Category> GetAll(GetAllParams options);
        Category GetOne(Guid id);
        Category UpdateOne(Guid id, Category Category);
        bool DeleteOneById(Guid id);
    }
}