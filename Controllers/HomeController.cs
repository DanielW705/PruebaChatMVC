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
        /********Metodos o funciones*******/
        /****Inyeccion de dependencias********/
        private readonly ILogger<HomeController> _logger;
        /*********Construtor**********/
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /*********Nodos************/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User usuario)
        {
            usuario.id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View("Privacy", model: usuario);
            }
        }
        public IActionResult MensajesChat([FromBody] string idUsuario)
        {
            return PartialView(model: idUsuario);
        }
    }
}
