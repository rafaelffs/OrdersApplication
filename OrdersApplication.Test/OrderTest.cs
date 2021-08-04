using Microsoft.EntityFrameworkCore;
using OrdersApplication.Database;
using OrdersApplication.Models;
using OrdersApplication.Services.Orders;
using OrdersApplication.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrdersApplication.Test
{
    public class OrderTest
    {
        private readonly IOrderService orderService;
        private Order orderMock = new Order();
        private OrdersApplicationDBContext context;

        public OrderTest()
        {
            var dbOption = new DbContextOptionsBuilder<Database.OrdersApplicationDBContext>()
                .UseInMemoryDatabase("OrdersApplicationDBTestOrder"+ new Guid())
                .Options;
            context = new OrdersApplicationDBContext(dbOption);
            context.Database.EnsureDeleted();
            this.orderService = new OrderService(context, new ProductService(context));
            Seed();

        }

        private async void Seed()
        {
            List<Product> productsMock = new List<Product>();
            productsMock.Add(new Product() { Name = "TV" });
            productsMock.Add(new Product() { Name = "Bed" });
            productsMock.Add(new Product() { Name = "Game" });

            orderMock.Location = new Location() { Address = "50th Avenue", PostalCode = "D010101" };
            orderMock.Products = productsMock;

            context.Products.AddRange(productsMock);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async void CreateOrder_Test()
        {
            //Create an order
            Order order = orderMock;
            Order orderSaved = await orderService.CreateOrder(order);
            Assert.Equal(order, orderSaved);
        }

        [Fact]
        public void CreateOrder_InvalidProduct_Test()
        {
            //Create an order
            Order order = orderMock;

            //Add Invalid product
            order.Products.Add(new Product() { Id = 0 });

            Assert.Throws<AggregateException>(() => orderService.CreateOrder(order).Result);
        }

        [Fact]
        public async void GetOrderById_Test()
        {
            context.Orders.Add(new Order() { Id = 1 });
            await context.SaveChangesAsync();
            Order order = await orderService.GetOrderById(1);
            Assert.Equal(1, order.Id);
        }

        [Fact]
        public async void UpdateOrderAddressById_Test()
        {
            context.Orders.Add(new Order() { Id = 1, Location = new Location() { Address = "Street 1", PostalCode = "D010101" } });
            await context.SaveChangesAsync();
            Order order = await orderService.UpdateOrderAddressById(1, new Location() { Address = "Street 2", PostalCode = "D020202" });
            Assert.Equal("Street 2", order.Location.Address);
        }

        [Fact]
        public async void CancelOrderById_Test()
        {
            context.Orders.Add(new Order() { Id = 1 });
            await context.SaveChangesAsync();
            Order order = await orderService.CancelOrderById(1);
            Assert.Equal(Enum.OrderStatusEnum.Cancelled, order.Status);
        }

        [Fact]
        public async void GetOrders_Test()
        {
            for (int i = 1; i < 20; i++)
            {
                context.Orders.Add(new Order() { Id = i, Location = new Location() { Address = $"Street {i}", PostalCode = $"D0000{i}" } });
            }
            await context.SaveChangesAsync();

            List<Order> orders = await orderService.GetOrders(3, 10);
            Assert.True(orders.Count == 10);
            Assert.Equal(4, orders[0].Id);
            Assert.Equal(13, orders[9].Id);
        }
    }
}
