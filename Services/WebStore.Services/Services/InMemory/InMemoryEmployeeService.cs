using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Services.InMemory
{
    public class InMemoryEmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = TestData.Employees;

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (_employees.Contains(employee)) return;

            employee.Id = _employees.Count == 0 ? 1 : _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
        }

        public void Edit(int id, Employee employee)
        {
            var oldEmployee = GetEmployee(id);

            if (oldEmployee is null || employee is null)
                throw new ArgumentNullException(nameof(Employee));

            oldEmployee.Age = employee.Age;
            oldEmployee.FirstName = employee.FirstName;
            oldEmployee.SecondName = employee.SecondName;
            oldEmployee.Patronymic = employee.Patronymic;
        }

        public bool Delete(int id)
        {
            var employee = GetEmployee(id);

            return !(employee is null) && _employees.Remove(employee);
        }

        public void SaveChanges()
        {
        }
    }
}