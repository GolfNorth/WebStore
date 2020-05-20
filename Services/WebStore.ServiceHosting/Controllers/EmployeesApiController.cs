using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesApiController(IEmployeeService employeeService) => _employeeService = employeeService;

        [HttpGet]
        public IEnumerable<Employee> GetEmployees() => _employeeService.GetEmployees();

        [HttpGet("{id}")]
        public Employee GetEmployee(int id) => _employeeService.GetEmployee(id);

        [HttpPost]
        public void Add([FromBody] Employee employee) => _employeeService.Add(employee);

        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee employee) => _employeeService.Edit(id, employee);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeeService.Delete(id);

        [NonAction]
        public void SaveChanges() => _employeeService.SaveChanges();
    }
}