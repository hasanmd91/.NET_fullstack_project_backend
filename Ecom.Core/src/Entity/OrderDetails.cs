using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.src.Entity
{
    public class OrderDetails : BaseEntity
    {
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}