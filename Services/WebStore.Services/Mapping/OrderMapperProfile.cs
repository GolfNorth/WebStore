using AutoMapper;
using WebStore.Domain.Dtos.Orders;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}