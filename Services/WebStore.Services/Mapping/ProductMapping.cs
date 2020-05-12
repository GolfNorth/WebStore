using System.Collections.Generic;
using System.Linq;

namespace WebStore.Services.Mapping
{
    public static class ProductMapping
    {
        public static ProductViewModel ToView(this Product p)
        {
            return new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand?.Name,
            };
        }

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p)
        {
            return p.Select(ToView);
        }
    }
}