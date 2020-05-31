using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebStore.Controllers;
using WebStore.Domain.Dtos.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;
using Xunit;

namespace WebStore.UnitTests.Controllers
{
    public class CatalogControllerTests
    {
        private static IMapper MakeMapper()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<ProductMapperProfile>();
            });
            
            return configuration.CreateMapper();
        }
        
        [Fact]
        public void View_WithId_ReturnsWithCorrectView()
        {
            #region Arrange

            const int expectedProductId = 1;
            const decimal expectedPrice = 10m;

            var expectedName = $"Product id {expectedProductId}";
            var expectedBrandName = $"Brand of product {expectedProductId}";

            var productService = Substitute.For<IProductService>();
            productService.GetProduct(Arg.Any<int>()).Returns(callInfo =>
            {
                var id = (int) callInfo[0];
                
                return new ProductDto
                {
                    Id = id,
                    Name = $"Product id {id}",
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 1,
                    Price = expectedPrice,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = $"Brand of product {id}"
                    },
                    Category = new CategoryDto
                    {
                        Id = 1,
                        Name = $"Section of product {id}"
                    }
                };
            });
            
            var controller = new CatalogController(productService, MakeMapper());

            #endregion

            #region Act

            var result = controller.View(expectedProductId);

            #endregion

            #region Assert

            var resultData = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(resultData.Model);

            Assert.Equal(expectedProductId, model.Id);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(expectedPrice, model.Price);
            Assert.Equal(expectedBrandName, model.Brand); 

            #endregion
        }

        [Fact]
        public void Index_ByDefault_ReturnsCorrectView()
        {
            var productService = Substitute.For<IProductService>();
            productService.GetProducts(Arg.Any<ProductFilter>()).Returns(new[]
            {
                new ProductDto
                {
                    Id = 1,
                    Name = "Product 1",
                    Order = 0,
                    Price = 10m,
                    ImageUrl = "Product1.png",
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "Brand of product 1"
                    },
                    Category = new CategoryDto
                    {
                        Id = 1,
                        Name = "Category of product 1"
                    }
                },
                new ProductDto
                {
                    Id = 2,
                    Name = "Product 2",
                    Order = 0,
                    Price = 20m,
                    ImageUrl = "Product2.png",
                    Brand = new BrandDto
                    {
                        Id = 2,
                        Name = "Brand of product 2"
                    },
                    Category = new CategoryDto
                    {
                        Id = 2,
                        Name = "Category of product 2"
                    }
                }
            });

            var controller = new CatalogController(productService, MakeMapper());

            const int expectedCategoryId = 1;
            const int expectedBrandId = 5;

            var result = controller.Index(expectedCategoryId, expectedBrandId);

            var resultData = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(resultData.ViewData.Model);

            Assert.Equal(2, model.Products.Count());
            Assert.Equal(expectedCategoryId, model.CategoryId);
            Assert.Equal(expectedBrandId, model.BrandId);
            Assert.Equal("Brand of product 1", model.Products.First().Brand);
        }
    }
}