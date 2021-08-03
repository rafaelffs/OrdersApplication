using System.Text.Json.Serialization;

namespace OrdersApplication.DTO
{
    public class ProductOrderDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }

    }
}
