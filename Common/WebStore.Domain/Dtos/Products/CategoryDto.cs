using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dtos.Products
{
    public class CategoryDto : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}