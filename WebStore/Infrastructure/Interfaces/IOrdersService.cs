using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Models;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders(string userName);
        Order GetOrderById(int id);
        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}
