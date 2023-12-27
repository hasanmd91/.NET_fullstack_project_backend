using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.src.Entity
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}