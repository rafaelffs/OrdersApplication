using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersApplication.DTO;
using OrdersApplication.Models;
using OrdersApplication.Services.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderService orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            this.mapper = mapper;
            this.orderService = orderService;
        }
        /// <summary>
        /// Create an order
        /// </summary>
        /// <returns>OrderDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("PostOrder")]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO order)
        {
            Order orderModel;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                orderModel = await orderService.CreateOrder(mapper.Map<Order>(order));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return mapper.Map<OrderDTO>(orderModel);
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        /// <returns>OrderDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetOrderById{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            Order order = await orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return mapper.Map<OrderDTO>(order);
        }

        /// <summary>
        /// Get orders paginated
        /// </summary>
        /// <returns>OrderDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders(int skip, int pageSize)
        {
            List<Order> orders = await orderService.GetOrders(skip, pageSize);
            if (orders == null)
            {
                return NotFound();
            }
            return mapper.Map<List<OrderDTO>>(orders);
        }

        /// <summary>
        /// Cancel order by ID
        /// </summary>
        /// <returns>OrderDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("CancelOrderById{id}")]
        public async Task<ActionResult<OrderDTO>> CancelOrderById(int id)
        {
            Order order = await orderService.CancelOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return mapper.Map<OrderDTO>(order);
        }

        /// <summary>
        /// Updated order delivery address by ID
        /// </summary>
        /// <returns>OrderDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("UpdateOrderAddressById")]
        public async Task<ActionResult<OrderDTO>> UpdateOrderAddressById(int id, Location location)
        {
            Order order = await orderService.UpdateOrderAddressById(id, location);
            if (order == null)
            {
                return NotFound();
            }
            return mapper.Map<OrderDTO>(order);
        }
    }
}
