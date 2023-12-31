using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.src.Entity
{
    public class Review : BaseEntity
    {
        public string Content { get; set; }
        public int Ratings { get; set; }

        public string Reviewer { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
