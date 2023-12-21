namespace Ecom.Core.src.Entity
{
    public class OrderDetails : BaseEntity
    {
        public int Quantity { get; set; }
        public required Guid ProductId { get; set; }
    }
}