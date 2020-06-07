using System.Collections.Generic;

namespace WebStore.Domain.Dtos.Products
{
    public class PageProductsDto
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public int TotalCount { get; set; }
    }
}