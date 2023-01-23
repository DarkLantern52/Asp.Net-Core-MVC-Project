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

//Startup.cs dosyasý, Asp.Net Core MVC projeninin baþlangýç noktasýdýr. Bu dosya, uygulamanýn nasýl çalýþtýrýlacaðýný ve hangi
//hizmetlerin kullanýlacaðýný tanýmlar.
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
            services.AddControllersWithViews(); // MVC kontrolcülerinin ve görünümlerin kullanýlmasýný saðlar.
            services.AddDbContext<PortalDbContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:NewsPortalConnection"]); //appsettings.json dosyasýndaki NewsPortalConnection dizelerine eriþilir.
            });
            services.AddScoped<IPortalRepository, EfPortalRepository>(); //Bu iki sýnýf arasýnda çalýþma zamanýnda bir baðlantý oluþur ve
                                                                         //veritabaný iþlemleri gerçekleþtirilir.
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDbContext<AppIdentityDbContext>(opts => {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:IdentityConnection"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //Uygulama yapýlandýrmasý gerçekleþtirilir. 
        {
            if (env.IsProduction())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseDeveloperExceptionPage(); //Geliþtirme sürecinde hata sayfalarýný gösterir. 
                app.UseStatusCodePages(); //HTTP durum kodlarýnýn görüntülenmesini saðlar. 
            }     
            app.UseStaticFiles(); //Statik dosyalarýn (resimler, CSS vb.) sunulmasýný saðlar. 

            //app.UseRouting() ve app.UseEndpoints() satýrlarý, rotalama yapýlandýrmasýný gerçekleþtirir ve varsayýlan kontrolcü
            //rotasýný haritalar.
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
