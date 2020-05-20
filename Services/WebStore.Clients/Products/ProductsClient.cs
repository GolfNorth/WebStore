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

        public IEnumerable<Category> GetCategories() => Get<IEnumerable<Category>>($"{ServiceAddress}/categories");

        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<Brand>>($"{ServiceAddress}/brands");

        public IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null) =>
            Post(ServiceAddress, Filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<IEnumerable<ProductDto>>()
                .Result;

        public ProductDto GetProduct(int id) => Get<ProductDto>($"{ServiceAddress}/{id}");

        public void Add(Product product) => throw new NotImplementedException();

        public void Edit(int id, Product product) => throw new NotImplementedException();

        public bool Delete(int id) => throw new NotImplementedException();

        public void SaveChanges() => throw new NotImplementedException();
    }
}
