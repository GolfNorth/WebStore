using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Dtos.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrdersService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, WebApi.Orders) { }

        public IEnumerable<OrderDto> GetUserOrders(string UserName) => Get<IEnumerable<OrderDto>>($"{ServiceAddress}/user/{UserName}");

        public OrderDto GetOrderById(int id) => Get<OrderDto>($"{ServiceAddress}/{id}");

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var response = Post($"{ServiceAddress}/{userName}", orderModel);

            return response.Content.ReadAsAsync<OrderDto>().Result;
        }
    }
}
