using Ecom.Core.src.Entity;

namespace Ecom.Service.src.DTO
{
    public class ReviewReadDTO
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public int Ratings { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }

    public class ReviewCreateDTO
    {

        public required string Content { get; set; }
        public int Ratings { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }

    public class ReviewUpdateDTO
    {

        public required string Content { get; set; }
        public int Ratings { get; set; }
    }
}