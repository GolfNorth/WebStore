using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductService : InMemoryEntityService<ProductViewModel>
    {
        public InMemoryProductService()
        {
            Entities = new List<ProductViewModel>
            {
                new ProductViewModel
                {
                    Id = 1,
                    Name = "Туалетная бумага",
                    Description = "Незаменимая вещь в период пандемии",
                    Price = 199.9m
                },
                new ProductViewModel
                {
                    Id = 2,
                    Name = "Гречка",
                    Description = "Еда на вес золота",
                    Price = 59.9m
                }
            };
        }
    }
}