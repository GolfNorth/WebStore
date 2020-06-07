using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly ICartStore _cartStore;


        public CartService(IProductService productService, ICartStore cartStore)
        {
            _productService = productService;
            _cartStore = cartStore;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
                return;

            cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            _cartStore.Cart.Items.Clear();
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem {ProductId = id, Quantity = 1});

            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter()
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
            }).Products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return r;
        }
    }
}