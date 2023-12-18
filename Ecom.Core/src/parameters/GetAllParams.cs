namespace Ecom.Core.src.parameters
{
    public class GetAllParams
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public string Search { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string SortOrder { get; set; }
    }

}