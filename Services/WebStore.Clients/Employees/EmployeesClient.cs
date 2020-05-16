using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeeService
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApi.Employees) { }

        public IEnumerable<Employee> GetEmployees() => Get<List<Employee>>(ServiceAddress);

        public Employee GetEmployee(int id) => Get<Employee>($"{ServiceAddress}/{id}");

        public void Add(Employee employee) => Post(ServiceAddress, employee);

        public void Edit(int id, Employee employee) => Put($"{ServiceAddress}/{id}", employee);

        public bool Delete(int id) => Delete($"{ServiceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
