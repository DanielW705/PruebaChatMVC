using LibreriaChatMVC.Hubs;
using LibreriaChatMVC.Models;
using LibreriaChatMVC.Ports.Primary;
using LibreriaChatMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PruebaChatMVC.Hubs;
using PruebaChatMVC.Models;
using System.Diagnostics;

namespace PruebaChatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginServices _loginServices;
        private readonly CancellationTokenSource _tokenSource;
        public HomeController(ILoginServices loginServices, IHostApplicationLifetime lifetime, IHubContext<NotificationHub, INotificationsClient> hubContext)
        {
            _loginServices = loginServices;
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(lifetime.ApplicationStopping);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
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
                var result = await _loginServices.TryToLoginAsync(usuario, _tokenSource.Token);
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
        [HttpGet]
        public async Task<IActionResult> LogOut([FromQuery] string idUsuario)
        {
            await _loginServices.LogOutAsync(idUsuario, _tokenSource.Token);
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UsuarioViewModel usuarioViewModel)
        {
            IActionResult output = View(usuarioViewModel);
            if (ModelState.IsValid)
            {
                var result = await _loginServices.RegisterAndLogInAsync(usuarioViewModel, _tokenSource.Token);
                if (result.Success)
                    output = RedirectToAction("Index", "Home");
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Error al registrar", item.Message);
                    }

            }
            return output;
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
