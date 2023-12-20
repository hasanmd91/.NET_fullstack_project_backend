namespace Ecom.Core.src.Entity
{
    public class Review : BaseEntity
    {
        public string Content { get; set; }
        public int Ratings { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
