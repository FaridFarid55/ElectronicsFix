using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore; 

namespace ElectronicsFix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ????? ????? ???????? ???????? ???????
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // ???? ????? ??????
                    options.AccessDeniedPath = "/Account/AccessDenied"; // ???? ?????? ???????
                });

            // ????? ????? ?????? ??????
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DepiProjectContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // ????? ??????? ??? ????? ????????
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DepiProjectContext>();
                context.Database.Migrate();
            }

            // ??????? ?? ???????
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ????? ???????? ????????
            app.UseAuthentication();
            app.UseAuthorization();

            // ????? ????????
            //app.MapControllerRoute(
            //    name: "areas",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            //app.MapControllerRoute(
            //    name: "admin",
            //    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
