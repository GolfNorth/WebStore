using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.Products) { }

        public IEnumerable<CategoryDto> GetCategories() => Get<IEnumerable<CategoryDto>>($"{ServiceAddress}/categories");
        public CategoryDto GetCategory(int id) => Get<CategoryDto>($"{ServiceAddress}/categories/{id}");

        public IEnumerable<BrandDto> GetBrands() => Get<IEnumerable<BrandDto>>($"{ServiceAddress}/brands");
        public BrandDto GetBrand(int id) => Get<BrandDto>($"{ServiceAddress}/brands/{id}");

        public PageProductsDto GetProducts(ProductFilter filter = null) =>
            Post(ServiceAddress, filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<PageProductsDto>()
                .Result;

        public ProductDto GetProduct(int id) => Get<ProductDto>($"{ServiceAddress}/{id}");

        public void Add(Product product) => throw new NotImplementedException();

        public void Edit(int id, Product product) => throw new NotImplementedException();

        public bool Delete(int id) => throw new NotImplementedException();

        public void SaveChanges() => throw new NotImplementedException();
    }
}
