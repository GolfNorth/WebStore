using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreDB _db;
        private readonly IMapper _mapper;

        public SqlProductService(WebStoreDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories.AsEnumerable();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands.AsEnumerable();
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsQueryable();

            if (filter.BrandId.HasValue)
                query = query.Where(p => p.BrandId.HasValue && p.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId.Equals(filter.CategoryId.Value));
            if (filter.Ids.Count > 0)
                query = query.Where(p => filter.Ids.Contains(p.Id));

            return query.AsEnumerable().Select(p => _mapper.Map<Product, ProductDto>(p));
        }

        public ProductDto GetProduct(int id)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);

            return _mapper.Map<Product, ProductDto>(product);
        }

        public void Add(Product entity)
        {
            if (entity.Id > 0) return;

            _db.Products.Add(entity);
        }

        public void Edit(int id, Product product)
        {
            if (GetProduct(id) is null) return;

            _db.Products.Update(product);
        }

        public bool Delete(int id)
        {
            var dbItem = GetProduct(id);

            if (dbItem == null) return false;
                
            _db.Entry(dbItem).State = EntityState.Deleted;

            return true;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}