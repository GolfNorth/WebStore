using System.Collections.Generic;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;

namespace WebStore.Interfaces.Services
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders(string userName);
        Order GetOrderById(int id);
        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}