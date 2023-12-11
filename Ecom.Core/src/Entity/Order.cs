namespace Ecom.Core.src.Entity
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public User User { get; set; }
        public IEnumerable<Product> Products { get; set; }



    }
}