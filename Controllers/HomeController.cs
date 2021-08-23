using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaChatMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaChatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View("Privacy", usuario.UserName);
            }
        }
        public IActionResult MensajesChat([FromBody] string idUsuario)
        {
            return PartialView(idUsuario);
        }
    }
}
