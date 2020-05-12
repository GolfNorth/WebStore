using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class EmployeeMapping
    {
        public static EmployeeViewModel ToView(this Employee e)
        {
            return new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                Patronymic = e.Patronymic,
                Age = e.Age
            };
        }

        public static Employee FromView(this EmployeeViewModel e)
        {
            return new Employee
            {
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                Patronymic = e.Patronymic,
                Age = e.Age
            };
        }
    }
}