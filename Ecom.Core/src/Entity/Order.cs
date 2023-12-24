using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecom.Core.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
