using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsQueryable();

            if (filter.BrandId.HasValue)
                query = query.Where(p => p.BrandId.HasValue && p.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId.Equals(filter.CategoryId.Value));
            if (filter.Ids.Count > 0)
                query = query.Where(p => filter.Ids.Contains(p.Id));

            return query.ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void AddNew(Product entity)
        {
            if (entity.Id > 0) return;

            _context.Products.Add(entity);
        }

        public void Delete(int id)
        {
            var dbItem = GetProduct(id);

            if (dbItem != null)
                _context.Entry(dbItem).State = EntityState.Deleted;
        }
    }
}
