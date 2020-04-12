using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        List<EmployeeViewModel> _employeees;

        public EmployeeController()
        {
            _employeees = new List<EmployeeViewModel>
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

        public IActionResult Index()
        {
            return View(_employeees);
        }

        public IActionResult Details(int id)
        {
            return View(_employeees.FirstOrDefault(e => e.Id == id));
        }
    }
}