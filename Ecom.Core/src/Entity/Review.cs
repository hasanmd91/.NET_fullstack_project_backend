using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecom.Core.src.Entity
{
    public class Review : BaseEntity
    {
        public string Content { get; set; }
        public int Ratings { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
