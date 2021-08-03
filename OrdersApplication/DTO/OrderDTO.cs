using OrdersApplication.Enum;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrdersApplication.DTO
{
    public class OrderDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public List<ProductOrderDTO> Products { get; set; }
        public LocationDTO Location { get; set; }
        public OrderStatusEnum Status { get; set; }


    }
}
