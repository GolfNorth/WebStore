using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    /// <summary>
    ///     Абстрактный класс базовой модели
    /// </summary>
    public abstract class BaseViewModel
    {
        /// <summary>
        ///     Идентификатор модели
        /// </summary>
        public int Id { get; set; }
    }
}
