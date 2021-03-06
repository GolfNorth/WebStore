﻿using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderedEntity
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        /// <summary>
        ///     Дочерние категори
        /// </summary>
        public List<CategoryViewModel> ChildCategories { get; set; }

        /// <summary>
        ///     Родительская категория
        /// </summary>
        public CategoryViewModel ParentCategory { get; set; }
    }
}