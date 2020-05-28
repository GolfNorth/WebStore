using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using Xunit;

namespace WebStore.Services.Tests.Services
{
    public class CartServiceTests
    {
        private readonly Cart _cart;
        private readonly ICartService _cartService;
        
        public CartServiceTests()
        {
            _cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };

            var productService = Substitute.For<IProductService>();
            productService.GetProducts(Arg.Any<ProductFilter>()).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 1.1m,
                    Order = 0,
                    ImageUrl = "Product1.png",
                    Brand = new BrandDto {Id = 1, Name = "Brand 1"},
                    Category = new CategoryDto {Id = 1, Name = "Section 1"}
                },
                new ProductDto
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 2.2m,
                    Order = 0,
                    ImageUrl = "Product2.png",
                    Brand = new BrandDto {Id = 2, Name = "Brand 2"},
                    Category = new CategoryDto {Id = 2, Name = "Section 2"}
                },
            });

            var cartStore = Substitute.For<ICartStore>();
            cartStore.Cart.Returns(_cart);

            _cartService = new CartService(productService, cartStore);
        }
        
        [Fact]
        public void CartClassItemsCountReturnsCorrectQuantity()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 },
                }
            };
            const int expectedCount = 4;

            var actualCount = cart.ItemsCount;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void CartViewModelReturnsCorrectItemsCount()
        {
            var cartViewModel = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    { new ProductViewModel {Id = 1, Name = "Product 1", Price = 0.5m}, 1 },
                    { new ProductViewModel {Id = 2, Name = "Product 2", Price = 1.5m}, 3 },
                }
            };
            const int expectedCount = 4;

            var actualCount = cartViewModel.ItemsCount;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void CartServiceAddToCartWorkCorrect()
        {
            _cart.Items.Clear();

            const int expectedId = 5;

            _cartService.AddToCart(expectedId);

            Assert.Equal(1, _cart.ItemsCount);

            Assert.Single(_cart.Items);
            Assert.Equal(expectedId, _cart.Items[0].ProductId);
        }

        [Fact]
        public void CartServiceRemoveFromCartRemoveCorrectItem()
        {
            const int itemId = 1;

            _cartService.RemoveFromCart(itemId);

            Assert.Single(_cart.Items);
            Assert.Equal(2, _cart.Items[0].ProductId);
        }

        [Fact]
        public void CartServiceRemoveAllClearCart()
        {
            _cartService.RemoveAll();

            Assert.Empty(_cart.Items);
        }

        [Fact]
        public void CartServiceDecrementCorrect()
        {
            const int itemId = 2;

            _cartService.DecrementFromCart(itemId);

            Assert.Equal(3, _cart.ItemsCount);
            Assert.Equal(2, _cart.Items.Count);
            Assert.Equal(itemId, _cart.Items[1].ProductId);
            Assert.Equal(2, _cart.Items[1].Quantity);
        }

        [Fact]
        public void CartServiceRemoveItemWhenDecrementTo0()
        {
            const int itemId = 1;

            _cartService.DecrementFromCart(itemId);

            Assert.Equal(3, _cart.ItemsCount);
            Assert.Single(_cart.Items);
        }

        [Fact]
        public void CartServiceTransformFromCartWorkCorrect()
        {
            var result = _cartService.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.1m, result.Items.First().Key.Price);
        }
    }
}