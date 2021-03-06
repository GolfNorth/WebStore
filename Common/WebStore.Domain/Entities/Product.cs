﻿using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Order { get; set; }

        [ForeignKey("CategoryId")] public virtual Category Category { get; set; }

        [ForeignKey("BrandId")] public virtual Brand Brand { get; set; }
    }
}