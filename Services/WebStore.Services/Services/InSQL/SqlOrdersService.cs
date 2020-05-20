using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dtos.Orders;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public SqlOrdersService(WebStoreDB db, UserManager<User> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName) => _db.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Where(x => x.User.UserName == userName)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .AsEnumerable();

        public OrderDto GetOrderById(int id) => _db.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault(x => x.Id == id);

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.OrderViewModel.Address,
                    Name = orderModel.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user
                };

                _db.Orders.Add(order);

                foreach (var item in orderModel.OrderItems)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == item.Id);

                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");

                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Order = order,
                        Product = product
                    };

                    _db.OrderItems.Add(orderItem);
                }

                _db.SaveChanges();
                transaction.Commit();

                return _mapper.Map<OrderDto>(order);
            }
        }
    }
}