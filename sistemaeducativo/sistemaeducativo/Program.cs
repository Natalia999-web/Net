using Microsoft.EntityFrameworkCore;
using sistemaeducativo.Models;
using Microsoft.AspNetCore.Identity;

namespace sistemaeducativo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //configurar a la bd
            builder.Services.AddDbContext<SistemaeducativoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SistemaeducativoContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();//agregar el identity

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();//mapeo vistas de identidad

            app.Run();
        }
    }
}
