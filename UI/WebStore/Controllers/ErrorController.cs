﻿using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
            ViewData["Code"] = 404;
            return code == 404 ? View("NotFound") : View();
        }
    }
}