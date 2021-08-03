using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersApplication.DTO;
using OrdersApplication.Models;
using OrdersApplication.Services.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace OrdersApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <returns>ProductDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseType(typeof(ProductDTO))]
        [HttpPost("PostProduct")]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product productModel = await productService.CreateProduct(mapper.Map<Product>(product));
            return mapper.Map<ProductDTO>(productModel);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <returns>ProductDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseType(typeof(ProductDTO))]
        [HttpGet("GetProductById{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            Product product = await productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return mapper.Map<ProductDTO>(product);
        }

        /// <summary>
        /// Get products paginated
        /// </summary>
        /// <returns>ProductDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseType(typeof(List<ProductDTO>))]
        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts(int skip, int pageSize)
        {
            List<Product> products = await productService.GetProducts(skip, pageSize);
            if (products == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ProductDTO>>(products);
        }

        /// <summary>
        /// Delete product by ID
        /// </summary>
        /// <returns>bool</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseType(typeof(bool))]
        [HttpDelete("DeleteProductById{id}")]
        public async Task<ActionResult<bool>> DeleteProductById(int id)
        {
            bool result = await productService.DeleteProductById(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <returns>ProductDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseType(typeof(ProductDTO))]
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, ProductDTO product)
        {
            Product updatedProduct = await productService.UpdateProduct(id, mapper.Map<Product>(product));
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return mapper.Map<ProductDTO>(updatedProduct);

        }
    }
}
