using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebStore.Controllers;
using WebStore.Domain.Dtos.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;
using WebStore.Interfaces.Services;
using Xunit;

namespace WebStore.Tests.Controllers
{
    public class CartControllerTests
    {
        [Fact]
        public void CheckOutModelStateInvalidReturnsViewModel()
        {
            var cartService = Substitute.For<ICartService>();
            var orderService = Substitute.For<IOrdersService>();

            var controller = new CartController(cartService, orderService);

            controller.ModelState.AddModelError("error", "InvalidModel");

            const string expectedModelName = "Test order";

            var result = controller.CheckOut(new OrderViewModel { Name = expectedModelName });

            var resultData = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(resultData.Model);

            Assert.Equal(expectedModelName, model.OrderViewModel.Name);
        }

        [Fact]
        public void CheckOutCallsServiceAndReturn_Redirect()
        {
            var cartService = Substitute.For<ICartService>();
            cartService.TransformCart().Returns(new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {new ProductViewModel {Name = "Product"}, 1}
                }
            });

            const int expectedOrderId = 1;
            var orderService = Substitute.For<IOrdersService>();
            orderService.CreateOrder(Arg.Any<CreateOrderModel>(), Arg.Any<string>()).Returns(new OrderDto
            {
                Id = expectedOrderId
            });

            var controller = new CartController(cartService, orderService)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1"), }))
                    }
                }
            };

            var result = controller.CheckOut(new OrderViewModel
            {
                Name = "Test",
                Address = "Address",
                Phone = "Phone"
            });

            var resultData = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(resultData.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), resultData.ActionName);
            Assert.Equal(expectedOrderId, resultData.RouteValues["id"]);
        }
    }
}