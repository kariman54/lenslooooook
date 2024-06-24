using lensLook.Pl.Settings;
using lensLook.Dal.Context;
using lensLook.Dal.Models;
using lensLook.Pl.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using lensLook.Bll;
using lensLook.Dal;
using lensLook.Bll.Services;

namespace lensLook.Pl
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


            builder.Services.AddDbContext<LensLookDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"));
            });
            //builder.Services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();







            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSetting"));

            builder.Services.AddTransient<IEmailSettings, EmailSettings>();

            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddScoped<IBasketRepo, BasketRepo>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IServicesRepo, ServicesRepo>();
            builder.Services.AddScoped<IRequestServices, RequestServicesRepo>();

            builder.Services.AddAutoMapper(typeof(maping));

            // Configuration of Account of Security Module 



            builder.Services.AddIdentity<user, IdentityRole>()

            .AddEntityFrameworkStores<LensLookDbContext>()   // To Active All Interfaces of Identity
            .AddDefaultTokenProviders(); //Used To Generate Token Provieder
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                    x =>
                    {
                        x.LoginPath = "account/Login";
                        x.AccessDeniedPath = "Home/Error";

                    }
                );



















            var app = builder.Build();



            using var Scope = app.Services.CreateScope(); // Make Scope Manually  -- that`s not Under Controller CLR
            var Services = Scope.ServiceProvider;
            var loggerFactorey = Services.GetRequiredService<ILoggerFactory>(); // Allow Dependency Logger Factorey 
            try
            {

                var Manager = Services.GetRequiredService<UserManager<user>>();
                var RoleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();
                var Context = Services.GetRequiredService<LensLookDbContext>();
                Context.Database.Migrate();

                await SeedData.Seed(Manager, RoleManager, Context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });









            app.Run();
        }
    }
}
