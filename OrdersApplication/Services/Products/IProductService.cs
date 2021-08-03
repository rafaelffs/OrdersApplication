using OrdersApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersApplication.Services.Products
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts(int skip, int pageSize);
        Task<Product> UpdateProduct(int id, Product product);
        Task<bool> DeleteProductById(int id);
        bool ProductExists(int id);
    }
}
