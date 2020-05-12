﻿using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapping
    {
        public static EmployeeViewModel ToView(this Employee e) => new EmployeeViewModel
        {
            Id = e.Id,
            FirstName = e.FirstName,
            SecondName = e.SecondName,
            Patronymic = e.Patronymic,
            Age = e.Age
        };

        public static Employee FromView(this EmployeeViewModel e) => new Employee
        {
            FirstName = e.FirstName,
            SecondName = e.SecondName,
            Patronymic = e.Patronymic,
            Age = e.Age
        };
    }
}
