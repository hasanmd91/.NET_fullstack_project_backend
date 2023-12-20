using Ecom.Core.src.Entity;

namespace Ecom.Service.src.DTO
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public UserReadDTO User { get; set; }
        public required IEnumerable<OrderDetails> OrderDetails { get; set; }
    }


    public class OrderCreateDTO
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public required IEnumerable<OrderDetails> OrderDetails { get; set; }
    }

}

