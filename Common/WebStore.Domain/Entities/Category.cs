using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("ParentId")] public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}