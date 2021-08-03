using Microsoft.EntityFrameworkCore;
using OrdersApplication.Database;
using OrdersApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApplication.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly OrdersApplicationDBContext dbContext;

        public ProductService(OrdersApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> GetProducts(int skip, int pageSize)
        {
            var products = await dbContext.Products
                .Skip(skip).Take(pageSize).ToListAsync();
            return products;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            product.Id = id;

            dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return product;
        }

        public bool ProductExists(int id)
        {
            return dbContext.Products.Any(e => e.Id == id);
        }

    }
}
