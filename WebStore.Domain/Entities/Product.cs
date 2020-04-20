using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    ///     Класс продукта
    /// </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
        /// <summary>
        ///     Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///     Идентификатор бренда
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        ///     URL изображения продута
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        ///     Цена продукта
        /// </summary>
        public decimal Price { get; set; }

        public int Order { get; set; }
    }
}