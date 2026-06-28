using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Ports.Secondary;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace PruebaChatMVC.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IIdentityRepository _identityServices;
        public HeaderViewComponent(IIdentityRepository identityServices)
        {
            _identityServices = identityServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {           
            return View(_identityServices.GetUserInformation());
        }
    }
}
