using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Dtos.Orders
{
    public class OrderItemDto : BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}