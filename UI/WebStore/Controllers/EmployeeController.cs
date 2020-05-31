using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Breadcrumb("Сотрудники")]
    [Route("employees")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeesService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeesService, IMapper mapper)
        {
            _employeesService = employeesService;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        // GET: /employees
        public IActionResult Index()
        {
            return View(_employeesService.GetEmployees().Select(_mapper.Map<EmployeeViewModel>));
        }

        [Breadcrumb("Просмотр")]
        [Route("{id}")]
        [Authorize(Roles = "Admins,Users")]
        // GET: /employees/{id}
        public IActionResult View(int id)
        {
            return View(_mapper.Map<EmployeeViewModel>(_employeesService.GetEmployee(id)));
        }

        [Breadcrumb("Редактирование")]
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

            return View(_mapper.Map<EmployeeViewModel>(model));
        }

        [Route("edit/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admins")]
        // GET: /employees/edit/{id}
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id > 0) // если есть Id, то редактируем модель
                _employeesService.Edit(model.Id, _mapper.Map<Employee>(model));
            else // иначе добавляем модель в список
                _employeesService.Add(_mapper.Map<Employee>(model));

            _employeesService.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [HttpGet]
        [Authorize(Roles = "Admins")]
        // GET: /employees/delete/{id}
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);
            _employeesService.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}