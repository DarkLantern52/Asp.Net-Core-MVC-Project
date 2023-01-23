using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using System.Linq;

namespace NewsPortal.Components
{
    public class SliderViewComponent : ViewComponent
    {
        private IPortalRepository repository;
        public SliderViewComponent(IPortalRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            var news = repository.Newscast.ToList();
            return View(news);
        }
    }
}

