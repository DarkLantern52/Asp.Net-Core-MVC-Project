using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewsPortal.Models.IPortalRepository;
using Microsoft.AspNetCore.Identity;
using SportsStore.Models;

//Startup.cs dosyas�, Asp.Net Core MVC projeninin ba�lang�� noktas�d�r. Bu dosya, uygulaman�n nas�l �al��t�r�laca��n� ve hangi
//hizmetlerin kullan�laca��n� tan�mlar.
namespace NewsPortal
{
    public class Startup
    {       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // MVC kontrolc�lerinin ve g�r�n�mlerin kullan�lmas�n� sa�lar.
            services.AddDbContext<PortalDbContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:NewsPortalConnection"]); //appsettings.json dosyas�ndaki NewsPortalConnection dizelerine eri�ilir.
            });
            services.AddScoped<IPortalRepository, EfPortalRepository>(); //Bu iki s�n�f aras�nda �al��ma zaman�nda bir ba�lant� olu�ur ve
                                                                         //veritaban� i�lemleri ger�ekle�tirilir.
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDbContext<AppIdentityDbContext>(opts => {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:IdentityConnection"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //Uygulama yap�land�rmas� ger�ekle�tirilir. 
        {
            if (env.IsProduction())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseDeveloperExceptionPage(); //Geli�tirme s�recinde hata sayfalar�n� g�sterir. 
                app.UseStatusCodePages(); //HTTP durum kodlar�n�n g�r�nt�lenmesini sa�lar. 
            }     
            app.UseStaticFiles(); //Statik dosyalar�n (resimler, CSS vb.) sunulmas�n� sa�lar. 

            //app.UseRouting() ve app.UseEndpoints() sat�rlar�, rotalama yap�land�rmas�n� ger�ekle�tirir ve varsay�lan kontrolc�
            //rotas�n� haritalar.
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catpage", "{category}/Sayfa{newsPage:int}",
                new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("page", "Sayfa{newsPage:int}",
                new { Controller = "Home", action = "Index", newsPage = 1 });

                endpoints.MapControllerRoute("category", "{category}",
                new { Controller = "Home", action = "Index", newsPage = 1 });

                endpoints.MapControllerRoute("pagination", "Haberler/Sayfa{newsPage}",
                new { Controller = "Home", action = "Index", newsPage = 1 });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}","/Admin/Index"); 
			});
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
