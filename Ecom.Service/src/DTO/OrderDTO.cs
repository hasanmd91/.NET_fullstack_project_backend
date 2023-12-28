using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;

namespace Ecom.Service.src.DTO
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public required UserReadDTO User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public required IEnumerable<OrderDetails> OrderDetails { get; set; }
    }


    public class OrderCreateDTO
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public required IEnumerable<OrderDetails> OrderDetails { get; set; }
    }


    public class OrderUpdateDTO
    {
        public OrderStatus OrderStatus { get; set; }
    }

}

