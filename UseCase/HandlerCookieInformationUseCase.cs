using Microsoft.AspNetCore.Http;
using ROP;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text;

namespace PruebaChatMVC.UseCase
{
    public class HandlerCookieInformationUseCase
    {
        private readonly IHttpContextAccessor _httpContext;


        private readonly CookieOptions _cookieOptions = new CookieOptions { HttpOnly = false, Expires = DateTime.Now.AddHours(24) };
        public HandlerCookieInformationUseCase(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        protected Result<Unit> SignUserId(Guid userId)
        {
            _httpContext.HttpContext.Response.Cookies.Append("UserId", JsonConvert.SerializeObject(new { IdUser = userId }), _cookieOptions);
            _httpContext.HttpContext.Session.SetString("UserId", userId.ToString());
            return Result.Unit;
        }
        protected Result<Guid> GetUserInformationCookie()
        {
            var definition = new { IdUser = Guid.Empty };

            string userData = string.Empty;
            Result<Guid> output = Guid.Empty;
            bool ExistCookie = _httpContext.HttpContext.Request.Cookies.TryGetValue("UserId", out userData);
            if (ExistCookie)
            {
                var data = JsonConvert.DeserializeAnonymousType(userData, definition);
                output = data.IdUser;
            }
            else
                output = Result.NotFound<Guid>("No se encontro el valor");
            return output;
        }
        protected Guid? GetUserInformationSession()
        {
            Guid? output = Guid.Empty;
            bool ExistCookie = _httpContext.HttpContext.Session.TryGetValue("UserId", out byte[] data);
            if (ExistCookie)
            {
                string userData = Encoding.Default.GetString(data);
                output = Guid.Parse(userData);
            }
            else
                output = null;
            return output;
        }
    }
}
