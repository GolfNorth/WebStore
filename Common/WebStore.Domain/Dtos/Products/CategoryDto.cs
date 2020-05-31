using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dtos.Products
{
    public class CategoryDto : INamedEntity
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }
        
        public int? ParentId { get; set; }
    }
}