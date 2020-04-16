using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IEntityService<ProductViewModel> _productService;

        public ProductController(IEntityService<ProductViewModel> productService)
        {
            _productService = productService;

        }

        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        [Route("{id}")]
        public IActionResult View(int id)
        {
            return View(_productService.GetById(id));
        }

        [Route("edit/{id?}")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new ProductViewModel());

            var model = _productService.GetById(id.Value);
            if (model == null)
                return NotFound();// возвращаем результат 404 Not Found


            return View(model);
        }

        [Route("edit/{id?}")]
        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _productService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.Name = model.Name;
                dbItem.Description = model.Description;
                dbItem.Price = model.Price;
            }
            else // иначе добавляем модель в список
            {
                _productService.AddNew(model);
            }
            _productService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}