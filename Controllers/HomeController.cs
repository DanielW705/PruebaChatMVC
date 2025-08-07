using Microsoft.AspNetCore.Mvc;
using PruebaChatMVC.UseCase;
using PruebaChatMVC.ViewModel;
using ROP;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace PruebaChatMVC.Controllers
{
    public class HomeController : Controller
    {
        /********Metodos o funciones*******/
        /****Inyeccion de dependencias********/
        private readonly HanderUserUseCase _userUseCase;
        /*********Construtor**********/
        public HomeController(HanderUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }
        /*********Nodos************/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRegisterUserViewModel usuario)
        {
            IActionResult output;
            if (ModelState.IsValid)
            {
                Result<Unit> result = await _userUseCase.Login(usuario);
                if (result.Success)
                    output = RedirectToAction("Index", "Home");
                else
                {
                    foreach (Error error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Message);
                    }
                    output = View(usuario);
                }
            }
            else
                output = View(usuario);
            return output;
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterUserViewModel usuario)
        {
            IActionResult output;
            if (ModelState.IsValid)
            {
                Result<Unit> result = await _userUseCase.RegisterNewuser(usuario);
                if (result.Success)
                    output = RedirectToAction("Index", "Home");
                else
                {
                    foreach (Error error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Message);
                    }
                    output = View(usuario);
                }
            }
            else
                output = View(usuario);
            return output;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ChatsViewModel viewModel = await _userUseCase.Execute().ThrowAsync();
            return View(viewModel);
        }
        //public IActionResult MensajesChat([FromForm] string idChat, [FromForm] string idReciber, [FromForm] string idSender)
        //{
        //    List<Messages> mensajesEnviados = DbModel.MensajesEnviados.Where(
        //        d => (d.Sender == Guid.Parse(idSender) || d.Reciber == Guid.Parse(idSender)) &&
        //        (d.Reciber == Guid.Parse(idReciber) || d.Sender == Guid.Parse(idReciber)))
        //        .Take(10).OrderBy(d => d.FechaDeEnvio).ToList();
        //    return PartialView(model: (idChat, mensajesEnviados, idSender));
        //}
    }
}
