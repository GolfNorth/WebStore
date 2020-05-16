using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Dtos.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrdersService
    {
        private readonly IOrdersService _ordersService;

        public OrdersApiController(IOrdersService orderService) => _ordersService = orderService;

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDto> GetUserOrders(string UserName) => _ordersService.GetUserOrders(UserName);

        [HttpGet("{id}")]
        public OrderDto GetOrderById(int id) => _ordersService.GetOrderById(id);

        [HttpPost("{UserName}")]
        public OrderDto CreateOrder([FromBody] CreateOrderModel orderModel, string userName) => _ordersService.CreateOrder(orderModel, userName);
    }
}