namespace Ecom.Core.src.Entity
{
    public class Review : BaseEntity
    {

        public string Content { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Range { get; set; }


    }
}