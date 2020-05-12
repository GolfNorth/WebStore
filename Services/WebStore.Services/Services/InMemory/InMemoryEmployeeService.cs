using System.Collections.Generic;
using System.Linq;

namespace WebStore.Services.Services.InMemory
{
    public class InMemoryEmployeeService : IEmployeeService
    {
        protected List<Employee> Entities;

        public InMemoryEmployeeService()
        {
            Entities = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "Иван",
                    SecondName = "Иванов",
                    Patronymic = "Иванович",
                    Age = 22
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Владислав",
                    SecondName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35
                }
            };
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return Entities;
        }

        public Employee GetEmployee(int id)
        {
            return Entities.FirstOrDefault(e => e.Id == id);
        }

        public void Commit()
        {
        }

        public void AddNew(Employee employee)
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