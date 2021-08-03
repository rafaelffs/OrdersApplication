using OrdersApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersApplication.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders(int skip, int pageSize);
        Task<Order> CancelOrderById(int id);
        Task<Order> UpdateOrderAddressById(int id, Location location);
    }
}
