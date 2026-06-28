using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Enums;
using LibreriaChatMVC.Extensions;
using LibreriaChatMVC.Ports.Secondary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PruebaChatMVC.Ports.Secundary
{
    public class IdentityRepository : IIdentityRepository
    {
        private IHttpContextAccessor _contextAccessor;
        public IdentityRepository(IHttpContextAccessor contextAccesor)
        {
            _contextAccessor = contextAccesor;
        }
        public UserDto? GetUserInformation()
        {
            UserDto? result = null;
            if (_contextAccessor.HttpContext?.User.Identity is not null && _contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var name = _contextAccessor.HttpContext.User.Identity.Name;
                var rol = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                if (userId is not null && name is not null && rol is not null)
                    result = new UserDto(userId, EnumExtension.ObtenerDesdeDescripcion<RolesType>(rol), name);
            }
            return result;
        }

        public async Task SignInUserAsync(string userId, string userName, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            if (_contextAccessor is not null && _contextAccessor.HttpContext is not null)
                await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task SingOutAsync()
        {
            if (_contextAccessor is not null && _contextAccessor.HttpContext is not null)
                await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
