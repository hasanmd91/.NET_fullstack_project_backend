using Ecom.Core.src.Entity;

namespace Ecom.Service.src.DTO
{
    public class ProductReadDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public required IEnumerable<Image> Images { get; set; }

    }

    public class ProductCreateDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public required IEnumerable<Image> Images { get; set; }

    }

    public class ProductUpdateDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required IEnumerable<Image> Images { get; set; }

    }

}