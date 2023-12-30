using Ecom.Core.src.Entity;

namespace Ecom.Service.src.DTO
{
    public class CategoryReadDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public class CategoryCreateDTO
    {
        public string Name { get; set; }
    }

    public class CategoryUpdateDTO
    {
        public string? Name { get; set; }

    }
}