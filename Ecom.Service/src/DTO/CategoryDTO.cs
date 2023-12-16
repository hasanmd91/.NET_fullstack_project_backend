using Ecom.Core.src.Entity;

namespace Ecom.Service.src.DTO
{
    public class CategoryReadDTO
    {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class CategoryCreateDTO
    {
        public string Name { get; set; }
    }

    public class CategoryUpdateDTO
    {
        public string Name { get; set; }

    }
}