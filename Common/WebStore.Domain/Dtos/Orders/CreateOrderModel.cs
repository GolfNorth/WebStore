using System.Collections.Generic;
using WebStore.Domain.ViewModels.Orders;

namespace WebStore.Domain.Dtos.Orders
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
