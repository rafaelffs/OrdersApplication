using OrdersApplication.Enum;
using System.Collections.Generic;

namespace OrdersApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public Location Location { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
