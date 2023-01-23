using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    public class NewsEditController : Controller
    {
        private readonly PortalDbContext _context;
        public NewsEditController(PortalDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Create(News news)
		{
            _context.Add(news);
            _context.SaveChanges();
			return RedirectToAction(nameof(Create));
		}


		[HttpGet]
		public IActionResult Edit(int Id)
		{
			var editNews = _context.Newscast.Find(Id);
			return View(editNews);
		}
		[HttpPost]
		public IActionResult Edit(News news)
		{
			_context.Update(news);
			_context.SaveChanges();
			return View("/Pages/Admin/Index.cshtml");
		}

        public async Task<IActionResult> Delete(int Id)
        {
            var deleteNews = await _context.Newscast.FindAsync(Id);
            _context.Remove(deleteNews);
            await _context.SaveChangesAsync();
            return View("/Pages/Admin/Index.cshtml");
        }
    }
}
