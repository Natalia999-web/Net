
using AngieGiraldo.Data;

using Microsoft.EntityFrameworkCore;

namespace AngieGiraldo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Validación de conexion de la Base de Datos
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // 🔒 Recomendado para producción
            }

            app.UseHttpsRedirection(); // ✅ Redirige HTTP a HTTPS
            app.UseStaticFiles();      // ✅ Sirve archivos estáticos (CSS, JS, imágenes, etc.)

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}