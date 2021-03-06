﻿using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;

namespace WebStore.Services.Data
{
    public class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                SecondName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 39
            },
            new Employee
            {
                Id = 2,
                SecondName = "Петров",
                FirstName = "Пётр",
                Patronymic = "Петрович",
                Age = 18
            },
            new Employee
            {
                Id = 3,
                SecondName = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 27
            },
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand {Id = 1, Name = "Acne", Order = 0},
            new Brand {Id = 2, Name = "Grune Erde", Order = 1},
            new Brand {Id = 3, Name = "Albiro", Order = 2},
            new Brand {Id = 4, Name = "Ronhill", Order = 3},
            new Brand {Id = 5, Name = "Oddmolly", Order = 4},
            new Brand {Id = 6, Name = "Boudestijn", Order = 5},
            new Brand {Id = 7, Name = "Rosch creative culture", Order = 6},
        };

        public static IEnumerable<Category> Categories { get; } = new[]
        {
            new Category {Id = 1, Name = "Спорт", Order = 0},
            new Category {Id = 2, Name = "Nike", Order = 0, ParentId = 1},
            new Category {Id = 3, Name = "Under Armour", Order = 1, ParentId = 1},
            new Category {Id = 4, Name = "Adidas", Order = 2, ParentId = 1},
            new Category {Id = 5, Name = "Puma", Order = 3, ParentId = 1},
            new Category {Id = 6, Name = "ASICS", Order = 4, ParentId = 1},
            new Category {Id = 7, Name = "Для мужчин", Order = 1},
            new Category {Id = 8, Name = "Fendi", Order = 0, ParentId = 7},
            new Category {Id = 9, Name = "Guess", Order = 1, ParentId = 7},
            new Category {Id = 10, Name = "Valentino", Order = 2, ParentId = 7},
            new Category {Id = 11, Name = "Диор", Order = 3, ParentId = 7},
            new Category {Id = 12, Name = "Версачи", Order = 4, ParentId = 7},
            new Category {Id = 13, Name = "Армани", Order = 5, ParentId = 7},
            new Category {Id = 14, Name = "Prada", Order = 6, ParentId = 7},
            new Category {Id = 15, Name = "Дольче и Габбана", Order = 7, ParentId = 7},
            new Category {Id = 16, Name = "Шанель", Order = 8, ParentId = 7},
            new Category {Id = 17, Name = "Гуччи", Order = 9, ParentId = 7},
            new Category {Id = 18, Name = "Для женщин", Order = 2},
            new Category {Id = 19, Name = "Fendi", Order = 0, ParentId = 18},
            new Category {Id = 20, Name = "Guess", Order = 1, ParentId = 18},
            new Category {Id = 21, Name = "Valentino", Order = 2, ParentId = 18},
            new Category {Id = 22, Name = "Dior", Order = 3, ParentId = 18},
            new Category {Id = 23, Name = "Versace", Order = 4, ParentId = 18},
            new Category {Id = 24, Name = "Для детей", Order = 3},
            new Category {Id = 25, Name = "Мода", Order = 4},
            new Category {Id = 26, Name = "Для дома", Order = 5},
            new Category {Id = 27, Name = "Интерьер", Order = 6},
            new Category {Id = 28, Name = "Одежда", Order = 7},
            new Category {Id = 29, Name = "Сумки", Order = 8},
            new Category {Id = 30, Name = "Обувь", Order = 9},
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product
            {
                Id = 1, Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0, CategoryId = 2,
                BrandId = 1
            },
            new Product
            {
                Id = 2, Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1, CategoryId = 2,
                BrandId = 1
            },
            new Product
            {
                Id = 3, Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2, CategoryId = 2,
                BrandId = 1
            },
            new Product
            {
                Id = 4, Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3, CategoryId = 2, BrandId = 1
            },
            new Product
            {
                Id = 5, Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4, CategoryId = 2,
                BrandId = 2
            },
            new Product
            {
                Id = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5,
                CategoryId = 2, BrandId = 1
            },
            new Product
            {
                Id = 7, Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6, CategoryId = 2,
                BrandId = 1
            },
            new Product
            {
                Id = 8, Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7, CategoryId = 25,
                BrandId = 1
            },
            new Product
            {
                Id = 9, Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8,
                CategoryId = 25, BrandId = 1
            },
            new Product
            {
                Id = 10, Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9, CategoryId = 25,
                BrandId = 3
            },
            new Product
            {
                Id = 11, Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10, CategoryId = 25,
                BrandId = 3
            },
            new Product
            {
                Id = 12, Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11, CategoryId = 25,
                BrandId = 3
            },
        };
    }
}