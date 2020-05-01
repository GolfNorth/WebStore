using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeViewModel> GetEmployees();

        EmployeeViewModel GetEmployee(int id);

        void Commit();

        void AddNew(EmployeeViewModel model);

        void Delete(int id);
    }
}
