using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    [Route("employees")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeesService;

        public EmployeeController(IEmployeeService employeesService)
        {
            _employeesService = employeesService;
        }

        [AllowAnonymous]
        // GET: /employees
        public IActionResult Index()
        {
            return View(_employeesService.GetEmployees().Select(e => e.ToView()));
        }

        [Route("{id}")]
        [Authorize(Roles = "Admins,Users")]
        // GET: /employees/{id}
        public IActionResult View(int id)
        {
            return View(_employeesService.GetEmployee(id).ToView());
        }

        [Route("edit/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admins")]
        // GET: /employees/edit/{id}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeViewModel());

            var model = _employeesService.GetEmployee(id.Value);

            if (model == null) return NotFound(); // возвращаем результат 404 Not Found

            return View(model.ToView());
        }

        [Route("edit/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admins")]
        // GET: /employees/edit/{id}
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _employeesService.GetEmployee(model.Id);

                if (dbItem == null)
                    return NotFound(); // возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.SecondName = model.SecondName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
            }
            else // иначе добавляем модель в список
            {
                _employeesService.AddNew(model.FromView());
            }

            _employeesService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [HttpGet]
        [Authorize(Roles = "Admins")]
        // GET: /employees/delete/{id}
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}