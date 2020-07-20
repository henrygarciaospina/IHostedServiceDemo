using IHostedServiceDemo.Contexts;
using IHostedServiceDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IHostedServiceDemo
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
            //Configuración del provider para bases de datos SQLServer
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient <Microsoft.Extensions.Hosting.IHostedService, ConsumeScopedService>();

            /*
             * Desde esta línea
               //Configuración de la clase WriteToFileHostedService (primer servicio)
               services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedService>();
               //Configuración de la clase WriteToFileHostedServiceSecond (segundo servicio)
               services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedServiceSecond>();
             * Hasta esta línea la comentareamos ya que son del Ejemplo 3 del Vídeo 35 - Ejecutar Código 
               Recurrente con IHostedService
            */
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}