using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private readonly IEntityService<EmployeeViewModel> _employeesService;

        public EmployeeController(IEntityService<EmployeeViewModel> employeesService)
        {
            _employeesService = employeesService;
        }

        // GET: /employees
        public IActionResult Index()
        {
            return View(_employeesService.GetAll());
        }

        [Route("{id}")]
        // GET: /employees/{id}
        public IActionResult View(int id)
        {
            return View(_employeesService.GetById(id));
        }

        [Route("edit/{id?}")]
        [HttpGet]
        // GET: /employees/edit/{id}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeViewModel());

            var model = _employeesService.GetById(id.Value);
            if (model == null)
                return NotFound();// возвращаем результат 404 Not Found


            return View(model);
        }

        [Route("edit/{id?}")]
        [HttpPost]
        // GET: /employees/edit/{id}
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _employeesService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Position = model.Position;
            }
            else // иначе добавляем модель в список
            {
                _employeesService.AddNew(model);
            }
            _employeesService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [HttpGet]
        // GET: /employees/delete/{id}
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}