using System.Collections.Generic;

namespace WebStore.Domain.Entities
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

        public List<int> Ids { get; set; } = new List<int>();
        
        public int Page { get; set; }

        public int? PageSize { get; set; }
    }
}