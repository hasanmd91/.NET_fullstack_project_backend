namespace Ecom.Core.src.Entity
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}