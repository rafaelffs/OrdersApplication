using AutoMapper;
using OrdersApplication.DTO;
using OrdersApplication.Models;

namespace OrdersApplication.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region .   DTO to Model   .

            CreateMap<LocationDTO, Location>();
            CreateMap<OrderDTO, Order>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductOrderDTO, Product>();

            #endregion

            #region .   Model to DTO   .

            CreateMap<Location, LocationDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductOrderDTO>();

            #endregion
        }
    }
}
