using Ecom.Core.src.Enum;

namespace Ecom.Core.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public User User { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
