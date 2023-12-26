using System.Text.Json.Serialization;

namespace Ecom.Core.src.Enum
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        PENDING,
        DELIVERED,
        RETURNED,
        CANCELED,
        PAID,
    }
}