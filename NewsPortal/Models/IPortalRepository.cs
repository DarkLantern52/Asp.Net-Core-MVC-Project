using System.Linq;
/*
IPortalRepository.cs dosyası, veritabanındaki haberleri temsil eden News model sınıfını kullanarak, veritabanındaki haberlerle
ilgili işlemleri gerçekleştirmek için kullanılacak bir arayüz oluşturmaktadır.
 */
namespace NewsPortal.Models
{
        public interface IPortalRepository //IPortalRepository adında bir arayüz oluşturur.
        {
            News GetById(int newsid);
            IQueryable<News> Newscast { get; } //Veritabanındaki tüm haberleri içeren bir sorgu oluşturur ve bu sorgunun sonucunu döndürür. 
            void SaveNews(News p);
            void CreateNews(News p);
            void DeleteNews(News p);
     

    }
}
