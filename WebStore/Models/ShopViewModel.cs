using System.Collections.Generic;

namespace WebStore.Models
{
    public class ShopViewModel
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
