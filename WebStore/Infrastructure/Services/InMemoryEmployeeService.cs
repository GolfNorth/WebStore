using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeeService : IEmployeeService
    {
        protected List<EmployeeViewModel> Entities;

        public InMemoryEmployeeService()
        {
            Entities = new List<EmployeeViewModel>
            {
                new EmployeeViewModel
                {
                    Id = 1,
                    FirstName = "Иван",
                    SurName = "Иванов",
                    Patronymic = "Иванович",
                    Age = 22,
                    Position = "Начальник"
                },
                new EmployeeViewModel
                {
                    Id = 2,
                    FirstName = "Владислав",
                    SurName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35,
                    Position = "Программист"
                }
            };
        }

        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            return Entities;
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            return Entities.FirstOrDefault(e => e.Id == id);
        }

        public void Commit()
        {
        }

        public void AddNew(EmployeeViewModel employee)
        {
            employee.Id = Entities.Max(e => e.Id) + 1;
            Entities.Add(employee);
        }

        public void Delete(int id)
        {
            var employee = GetEmployee(id);

            if (employee is null)
                return;

            Entities.Remove(employee);
        }
    }
}