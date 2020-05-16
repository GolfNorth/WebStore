using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        void Add(Employee employee);
        void Edit(int id, Employee employee);
        bool Delete(int id);
        void SaveChanges();
    }
}