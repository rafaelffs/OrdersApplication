using System.Text.Json.Serialization;

namespace OrdersApplication.DTO
{
    public class ProductDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
