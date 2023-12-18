namespace Ecom.Core.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
