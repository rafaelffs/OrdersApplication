
using System.Text.Json.Serialization;

namespace OrdersApplication.DTO
{
    public class LocationDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}
