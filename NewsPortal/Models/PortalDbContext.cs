using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
/*
PortalDbContext.cs dosyası, veritabanına bağlantı kurmak ve veritabanı işlemlerini gerçekleştirmek için kullanılan Entity Framework Core
sınıfını temsil eder. Bu sınıf, veritabanındaki haberleri temsil eden News model sınıfını kullanarak, veritabanındaki haberlerle ilgili
işlemleri gerçekleştirir.
 */
namespace NewsPortal.Models
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options): base(options) { }
        public DbSet<News> Newscast { get; set; } //Veritabanındaki haberleri temsil eden bir tabloyu temsil eder.

    }
}
