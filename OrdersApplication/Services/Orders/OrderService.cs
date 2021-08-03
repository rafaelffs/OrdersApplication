using OrdersApplication.Database;
using OrdersApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersApplication.Services.Products;

namespace OrdersApplication.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly OrdersApplicationDBContext dbContext;
        private readonly IProductService productService;

        public OrderService(OrdersApplicationDBContext dbContext, IProductService productService)
        {
            this.dbContext = dbContext;
            this.productService = productService;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            for (int i = 0; i < order.Products.Count; i++)
            {
                if (productService.ProductExists(order.Products[i].Id))
                    order.Products[i] = productService.GetProductById(order.Products[i].Id).Result;
                else
                    return null;
            }
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await dbContext.Orders
                .Include(prod => prod.Products)
                .Include(loc => loc.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<List<Order>> GetOrders(int skip, int pageSize)
        {
            var orders = await dbContext.Orders
                .Include(prod => prod.Products)
                .Include(loc => loc.Location)
                .Skip(skip).Take(pageSize).ToListAsync();
            return orders;
        }

        public async Task<Order> CancelOrderById(int id)
        {
            Order order = await dbContext.Orders.FindAsync(id);
            if (!OrderExists(id))
            {
                return null;
            }
            order.Status = Enum.OrderStatusEnum.Cancelled;
            dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return order;
        }

        private bool OrderExists(int id)
        {
            return dbContext.Orders.Any(e => e.Id == id);
        }

        public async Task<Order> UpdateOrderAddressById(int id, Location location)
        {
            Order order = await dbContext.Orders.FindAsync(id);
            if (!OrderExists(id))
            {
                return null;
            }
            order.Location = location;
            dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return order;
        }
    }
}
