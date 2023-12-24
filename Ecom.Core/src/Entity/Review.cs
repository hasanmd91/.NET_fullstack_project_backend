using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.src.Entity
{
    public class Review : BaseEntity
    {
        public string Content { get; set; }
        public int Ratings { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
    }
}
