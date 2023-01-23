using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace NewsPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            PortalDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<PortalDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Newscast.Any())
            {
                context.Newscast.AddRange(
                new News
                {
                    Title = "Başlık 1",
                    Image = "#",
                    Description = "Açıklama 1",
                    Category= "Spor",
                    Content = "İçerikte bulunan yazılar 1",
                },
                new News
                {
                    Title = "Başlık 2",
                    Image = "#",
                    Description = "Açıklama 2",
                    Category = "Savaş",
                    Content = "İçerikte bulunan yazılar 2",
                },
                new News
                {
                     Title = "Başlık 3",
                     Image = "#",
                     Description = "Açıklama 3",
                    Category = "Spor",
                    Content = "İçerikte bulunan yazılar 3",
                },
                new News
                {
                        Title = "Başlık 4",
                        Image = "#",
                        Description = "Açıklama 4",
                        Category ="Eğitim",
                        Content = "İçerikte bulunan yazılar 4",
                },
                new News
                {
                    Title = "Başlık 5",
                    Image = "#",
                     Description = "Açıklama 5",
                     Category ="Eğitim",
                     Content = "İçerikte bulunan yazılar 5",
                },
                new News
                {
                          Title = "Başlık 6",
                          Image = "#",
                          Description = "Açıklama 6",
                          Category ="Spor",
                          Content = "İçerikte bulunan yazılar 6",
                },
                new News
                {
                        Title = "Başlık 7",
                        Image = "#",
                        Description = "Açıklama 7",
                        Category ="Siyaset",
                        Content = "İçerikte bulunan yazılar 7",
                },
                new News
                {
                         Title = "Başlık 8",
                         Image = "#",
                         Description = "Açıklama 8",
                         Category ="Siyaset",
                         Content = "İçerikte bulunan yazılar 8",
                },
                new News
                {
                         Title = "Başlık 9",
                         Image = "#",
                         Description = "Açıklama 9",
                         Category ="Ekonomi",
                         Content = "İçerikte bulunan yazılar 9",
                }
                );
                context.SaveChanges();
            }
        }
    }
}
