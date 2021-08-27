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
        public string limpiarGuid(string GuidCon)
        {
            /***Se guarda lo que es despues del :***/
            string GuidLimpiando = GuidCon.Split(":")[1];
            /********Un string limpio*********/
            string GuidLimipo = string.Empty;
            /*******Se vuelve a barrer***********/
            foreach (char GuidCaracter in GuidLimpiando)
            {
                /********Analiza si es letra o digito********/
                if (char.IsLetterOrDigit(GuidCaracter))
                {
                    GuidLimipo += GuidCaracter;
                }

            }
            /*******Hace el parseo**********/
            return GuidLimipo;
        }
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View("Privacy", usuario.UserName);
            }
        }
        public IActionResult MensajesChat([FromBody] object idUsuario)
        {
            string GuidDelUsuario = limpiarGuid(idUsuario.ToString());
            return PartialView(model: GuidDelUsuario.ToString());
        }
    }
}
