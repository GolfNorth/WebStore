using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public IEnumerable<CategoryDto> GetCategories()
        {
            return _db.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }
        
        public CategoryDto GetCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            return _mapper.Map<CategoryDto>(category);
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _db.Brands
                .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }
        
        public BrandDto GetBrand(int id)
        {
            var brand = _db.Brands.FirstOrDefault(b => b.Id == id);

            return _mapper.Map<BrandDto>(brand);
        }

        public PageProductsDto GetProducts(ProductFilter filter = null)
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

            var totalCount = query.Count();
            if (filter?.PageSize != null)
            {
                query = query
                    .Skip((filter.Page - 1) * (int) filter.PageSize)
                    .Take((int) filter.PageSize);
            }

            return new PageProductsDto
            {
                Products = query.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).AsEnumerable(),
                TotalCount = totalCount
            };
        }

        public ProductDto GetProduct(int id) => _db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault(p => p.Id == id);

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