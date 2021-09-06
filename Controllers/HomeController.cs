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
        private readonly Model DbModel;
        /*********Construtor**********/
        public HomeController(ILogger<HomeController> logger, Model _DbModel)
        {
            DbModel = _DbModel;
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

            if (!ModelState.IsValid)
            {
                return View(usuario);
            }
            else if (!DbModel.Usuario.Where(d => d.UserName == usuario.UserName && d.Pasword == usuario.Pasword).Any())
            {
                ModelState.AddModelError("UserNotExist", "El Usuario no existe");
                return View(usuario);
            }
            else
            {
                usuario = DbModel.Usuario.Where(d => d.UserName == usuario.UserName && d.Pasword == usuario.Pasword).First();
                return View("Privacy", model: usuario);
            }
        }
        public IActionResult MensajesChat([FromForm] string idChat, [FromForm] string idMio, [FromForm] string idEl)
        {
            List<MessageSended> mensajesEnviados = DbModel.MensajesEnviados
                .Where(d => (d.Reciber == Guid.Parse(idMio) && d.Sender == Guid.Parse(idEl)) || 
                (d.Reciber == Guid.Parse(idEl) && d.Sender == Guid.Parse(idMio))).OrderBy(d=> d.FechaDeEnvio).ToList();
            return PartialView(model: (idChat, mensajesEnviados, idMio));
        }
    }
}
