using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using System.Linq;

namespace NewsPortal.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IPortalRepository repository;
        public NavigationMenuViewComponent(IPortalRepository repo)
        {
            repository = repo;
        }


        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Newscast
                .Select(x=>x.Category)
                .Distinct()
                .OrderBy(x=>x));
        }
    }
}
