using Microsoft.AspNetCore.Authentication.Cookies;
using VCMApp.Application;
using VCMApp.Infrastructure;

namespace VCMApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddRazorPages();
            builder.Services.AddSession();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
               {
                    options.LoginPath = "/Admin/Login";
                    options.AccessDeniedPath = "/Admin/AccessDenied";
               });

            var app = builder.Build();

            app.ApplicationStart();
            app.InfrastructureStart();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
