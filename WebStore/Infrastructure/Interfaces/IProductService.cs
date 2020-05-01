using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter);
        Product GetProduct(int id);
        void Commit();
        void AddNew(Product product);
        void Delete(int id);
    }
}