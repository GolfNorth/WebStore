using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Services.InMemory
{
    public class InMemoryEmployeeService : IEmployeeService
    {
        private readonly List<Employee> _entities = TestData.Employees;

        public IEnumerable<Employee> GetEmployees()
        {
            return _entities;
        }

        public Employee GetEmployee(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public void Commit()
        {
        }

        public void AddNew(Employee employee)
        {
            employee.Id = _entities.Max(e => e.Id) + 1;
            _entities.Add(employee);
        }

        public void Delete(int id)
        {
            var employee = GetEmployee(id);

            if (employee is null)
                return;

            _entities.Remove(employee);
        }
    }
}