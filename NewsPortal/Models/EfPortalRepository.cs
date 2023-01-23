using System;
using System.Linq;
/*
EfPortalRepository.cs dosyası, IPortalRepository arayüzünü uygulayan bir sınıf oluşturmaktadır. Bu sınıf, veritabanındaki haberlerle 
ilgili işlemleri gerçekleştirmek için Entity Framework kullanır
 */
namespace NewsPortal.Models
{
    public class EfPortalRepository : IPortalRepository
    {
        private PortalDbContext context; //Veritabanına bağlantı kurmak için gerekli olan hizmetleri içermektedir.
        public EfPortalRepository(PortalDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<News> Newscast => context.Newscast;

        public News GetById(int newsid)
        {
            return context.Newscast.Find(newsid);
        }
        public void CreateNews(News p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void DeleteNews(News p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public void SaveNews(News p)
        {
            context.SaveChanges();
        }
    }
}
