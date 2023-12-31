using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecom.Core.src.Entity
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }


        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

    }
}