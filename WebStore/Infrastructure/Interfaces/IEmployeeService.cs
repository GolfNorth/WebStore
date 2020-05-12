using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
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
