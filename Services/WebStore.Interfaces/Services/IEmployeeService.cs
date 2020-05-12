using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();

        Employee GetEmployee(int id);

        void Commit();

        void AddNew(Employee model);

        void Delete(int id);
    }
}