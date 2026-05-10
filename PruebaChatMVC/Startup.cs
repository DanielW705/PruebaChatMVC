using Microsoft.AspNetCore.CookiePolicy;

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
            app.UseAuthorization();
            app.UseEndpoints(endopints =>
            {
                endopints.MapControllerRoute("default","{controller=Home}/{action=Index}");
            });
        }
    }
}