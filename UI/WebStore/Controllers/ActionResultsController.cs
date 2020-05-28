using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    public class ActionResultsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ActionResultsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MergeContentString(string name)
        {
            return Content($"Hi, {name}. I'm mere string content result");
        }

        /// <summary>
        /// Returns nothing and StatusCode = 200
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Returns nothing and StatusCode = 204
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing204()
        {
            return new NoContentResult();
        }

        public JsonResult JsonObjectSerialized()
        {
            var employee = new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Иван",
                SecondName = "Иванов",
                Patronymic = "Иванович",
                Age = 22
            };

            return Json(employee);
        }

        public IActionResult GoGoogle()
        {
            return Redirect("https://google.com");
        }

        public IActionResult GoToHomePage()
        {
            return LocalRedirect("~/Home/Index");
        }

        public IActionResult RedirectWithParameters()
        {
            return RedirectToAction("MergeContentString", "ActionResults", new {name = "Dear user"});
        }

        public IActionResult ForbiddenResource()
        {
            //return Forbid(); // the same
            return StatusCode(403);
        }

        public IActionResult NotFoundResource()
        {
            //return StatusCode(404, "Nothing found. Sorry.");
            return NotFound("Nothing found. Sorry.");
        }

        public IActionResult AgeCheck(int age)
        {
            if (age < 18)
                return Unauthorized("Sorry. Adults only");

            return Content("You're welcome");
        }

        public IActionResult TellMeItsOk()
        {
            return Ok("Everything is gonna be fine!");
        }

        public IActionResult ReallyBadRequest(string s)
        {
            if (string.IsNullOrEmpty(s))
                return BadRequest("Some parameter was expected");

            return View("Index");
        }

        public IActionResult GetFile()
        {
            // Путь к файлу
            var file_path = Path.Combine(_webHostEnvironment.WebRootPath, "images/blog/man-two.jpg");
            // Тип файла - content-type
            var file_type = "image/jpeg";
            // Имя файла - необязательно
            var file_name = "My awesome ring.jpg";
            return PhysicalFile(file_path, file_type, file_name);
        }

        // Отправка массива байтов
        public FileResult GetBytes()
        {
            var file_path = Path.Combine(_webHostEnvironment.WebRootPath, "images/blog/man-two.jpg");
            var mas = System.IO.File.ReadAllBytes(file_path);
            var file_type = "image/jpeg";
            var file_name = "My awesome ring.jpg";
            return File(mas, file_type, file_name);
        }

        // Отправка потока
        public FileResult GetStream()
        {
            var file_path = Path.Combine(_webHostEnvironment.WebRootPath, "images/blog/man-two.jpg");
            var fs = new FileStream(file_path, FileMode.Open);
            var file_type = "image/jpeg";
            var file_name = "My awesome ring.jpg";
            var file = File(fs, file_type, file_name);
            
            fs.Close();

            return file;
        }
    }
}