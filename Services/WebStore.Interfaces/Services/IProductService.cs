using System.Collections.Generic;

namespace WebStore.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter);
        Product GetProductById(int id);
        void Commit();
        void AddNew(Product product);
        void Delete(int id);
    }
}