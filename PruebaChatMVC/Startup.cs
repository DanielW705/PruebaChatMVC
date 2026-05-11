using LibreriaChatMVC.Data;
using LibreriaChatMVC.Ports.Primary;
using LibreriaChatMVC.Ports.Secondary;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using PruebaChatMVC.Ports.Primary;
using PruebaChatMVC.Ports.Secundary;

namespace PruebaChatMVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
             _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = options => true;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });
            services.AddControllersWithViews();
            string conexion = _configuration.GetConnectionString("PruebaChatMVContext") ?? throw new NullReferenceException("Es necesaria una cadena de conexion");
            services.AddDbContext<PruebaChatMVContext>(options => options.UseSqlServer(conexion));
            services.AddLogging();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Home/Login";
                        //options.AccessDeniedPath =""
                        options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    });
            services.AddHttpContextAccessor();
            services.AddSingleton<IIdentityRepository, IdentityRepository>();
            services.AddTransient<IValidateUserRepository, ValidateUserRepository>();
            services.AddTransient<ILoginServices, LoginServices>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endopints =>
            {
                endopints.MapControllerRoute("default","{controller=Home}/{action=Login}");
            });
        }
    }
}