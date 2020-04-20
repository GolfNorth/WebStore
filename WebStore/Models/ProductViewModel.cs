using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Models
{
    public class ProductViewModel : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}