using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    ///     Класс категории
    /// </summary>
    public class Category : NamedEntity, IOrderedEntity
    {
        /// <summary>
        ///     Родительская категория
        /// </summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }
    }
}