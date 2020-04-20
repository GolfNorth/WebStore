﻿namespace WebStore.Domain
{
    /// <summary>
    ///     Класс для фильтрации товаров
    /// </summary>
    public class ProductFilter
    {
        /// <summary>
        ///     Категория фильтра
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///     Бренд фильтра
        /// </summary>
        public int? BrandId { get; set; }
    }
}