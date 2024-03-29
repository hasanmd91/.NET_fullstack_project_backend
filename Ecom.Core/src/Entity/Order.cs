using System.Text.Json.Serialization;
using Ecom.Core.src.Enum;

namespace Ecom.Core.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public OrderStatus OrderStatus { get; set; }
    }
}
