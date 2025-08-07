using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaChatMVC.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaChatMVC.LoggerProvider;
using PruebaChatMVC.Data;
using PruebaChatMVC.UseCase;
using Microsoft.AspNetCore.Http;

namespace PruebaChatMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddProvider(new FileLoggerProvider("apps.logs"));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = options => true;
            });

            services.AddControllersWithViews();

            services.AddSignalR();

            string conexion = Configuration.GetConnectionString("ChatPruebaConnectionString");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ChatPruebaDbContext>(options => options.UseSqlServer(conexion));

            services.AddSingleton<HandlerCookieInformationUseCase>();

            services.AddScoped<HanderUserUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //loggerfactory.AddProvider(new FileLoggerProvider("apps.logs"));

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
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
                Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
                endpoints.MapHub<ChatHub>("/chatHub");

            });
        }
    }
}
