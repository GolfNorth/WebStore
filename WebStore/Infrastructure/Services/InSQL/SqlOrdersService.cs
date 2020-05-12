using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Infrastructure.Services
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreDB db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(x => x.User.UserName == userName)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefault(x => x.Id == id);
        }

        public Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };

                _db.Orders.Add(order);

                foreach (var item in transformCart.Items)
                {
                    var productVm = item.Key;
                    var product = _db.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));

                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");

                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        Order = order,
                        Product = product
                    };

                    _db.OrderItems.Add(orderItem);
                }

                _db.SaveChanges();
                transaction.Commit();

                return order;
            }
        }
    }
}
