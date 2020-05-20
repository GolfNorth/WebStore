using System.Collections.Generic;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        IEnumerable<ProductDto> GetProducts(ProductFilter filter);
        ProductDto GetProduct(int id);
        void Add(Product product);
        void Edit(int id, Product product);
        bool Delete(int id);
        void SaveChanges();
    }
}