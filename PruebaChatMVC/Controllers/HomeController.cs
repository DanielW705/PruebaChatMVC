using LibreriaChatMVC.Ports.Primary;
using LibreriaChatMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaChatMVC.Models;
using System.Diagnostics;

namespace PruebaChatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginServices _loginServices;
        public HomeController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }
        [HttpGet]
        public IActionResult Login()
        {
            IActionResult output = View();
            if (HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated)
                output = RedirectToAction("Index", "Home");
            return output;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel usuario)
        {
            IActionResult output = View(usuario);
            if (ModelState.IsValid)
            {
                var result = await _loginServices.TryToLogin(usuario);
                if (result.Success)
                    output = RedirectToAction("Index", "Home");
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Usuario no encontrado", item.Message);
                    }
            }
            return output;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
