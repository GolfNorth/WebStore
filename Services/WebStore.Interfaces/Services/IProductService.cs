using System.Collections.Generic;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategory(int id);
        IEnumerable<BrandDto> GetBrands();
        BrandDto GetBrand(int id);
        PageProductsDto GetProducts(ProductFilter filter = null);
        ProductDto GetProduct(int id);
        void Add(Product product);
        void Edit(int id, Product product);
        bool Delete(int id);
        void SaveChanges();
    }
}