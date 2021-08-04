using Microsoft.EntityFrameworkCore;
using OrdersApplication.Database;
using OrdersApplication.Models;
using OrdersApplication.Services.Orders;
using OrdersApplication.Services.Products;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrdersApplication.Test
{
    public class ProductTest
    {
        private readonly IProductService productService;
        private Product productMock = new Product();
        private OrdersApplicationDBContext context;

        public ProductTest()
        {
            var dbOption = new DbContextOptionsBuilder<Database.OrdersApplicationDBContext>()
                .UseInMemoryDatabase("OrdersApplicationDBTestProduct" + new Guid())
                .Options;
            context = new OrdersApplicationDBContext(dbOption);
            context.Database.EnsureDeleted();
            this.productService = new ProductService(context);
            Seed();
        }
        private void Seed()
        {
            productMock = new Product() { Id = 1, Name = "DVD" };
        }

        [Fact]
        public async void CreateProduct_Test()
        {
            Product productSaved = await productService.CreateProduct(productMock);
            Assert.Equal(productMock, productSaved);
        }

        [Fact]
        public async void DeleteProductById_Test()
        {
            context.Products.Add(productMock);
            context.SaveChanges();
            Assert.True(await productService.DeleteProductById(1));
            Assert.Null(await context.Products.FindAsync(1));
        }

        [Fact]
        public async void GetProductById_Test()
        {
            context.Products.Add(productMock);
            context.SaveChanges();
            Product product = await productService.GetProductById(1);
            Assert.Equal(productMock, product);
        }

        [Fact]
        public async void GetProducts_Test()
        {
            for (int i = 1; i < 20; i++)
            {
                context.Products.Add(new Product() { Id = i });
            }
            await context.SaveChangesAsync();

            List<Product> products = await productService.GetProducts(3, 10);
            Assert.True(products.Count == 10);
            Assert.Equal(4, products[0].Id);
            Assert.Equal(13, products[9].Id);
        }

        [Fact]
        public async void UpdateProduct_Test()
        {
            context.Products.Add(productMock);
            await context.SaveChangesAsync();
            context.Entry(productMock).State = EntityState.Detached;

            Product product = new Product() { Name = "VideoGame" };
            Product updatedProduct = await productService.UpdateProduct(1, product);
            Assert.Equal(product, updatedProduct);
        }

    }
}
