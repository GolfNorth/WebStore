using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using WebStore.Domain.Dtos.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Breadcrumb("Корзина")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;

        public CartController(ICartService cartService, IOrdersService ordersService)
        {
            _cartService = cartService;
            _ordersService = ordersService;
        }

        public IActionResult Index()
        {
            var model = new OrderDetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };

            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderModel = new CreateOrderModel
                {
                    OrderViewModel = model,
                    OrderItems = _cartService.TransformCart().Items
                        .Select(item => new OrderItemDto()
                        {
                            Id = item.Key.Id,
                            Price = item.Key.Price,
                            Quantity = item.Value
                        })
                        .ToList()
                };

                var order = _ordersService.CreateOrder(orderModel, User.Identity.Name);

                _cartService.RemoveAll();
                return RedirectToAction("OrderConfirmed", new {id = order.Id});
            }

            var detailsModel = new OrderDetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };

            return View("Index", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            @ViewBag.OrderId = id;
            return View();
        }
    }
}