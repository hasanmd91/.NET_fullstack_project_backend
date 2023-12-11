using System.Text.Json.Serialization;

namespace Ecom.Core.src.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        User,
    }
}