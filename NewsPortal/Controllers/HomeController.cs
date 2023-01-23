using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    public class HomeController : Controller
    {
        private IPortalRepository repository;
        public int PageSize = 6;


        public HomeController(IPortalRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index(string category,int newsPage=1)
        {
            return View(new NewscastListViewModel
            {
                Newscast =repository.Newscast
                .Where(p=> category == null || p.Category==category)
                .OrderBy(p=>p.Id)
                .Skip((newsPage-1)*PageSize)
                .Take(PageSize),
                PagingInfo=new PagingInfo
                {
                    CurrentPage=newsPage,
                    ItemsPerPage=PageSize,
                    TotalItems=category==null?
                    repository.Newscast.Count():
                    repository.Newscast.Where(e=>
                    e.Category==category).Count()
                },
                CurrentCategory=category
            });
        }


        public IActionResult NewsInfo(int id)
        {
            return View(repository.GetById(id));
        }
        [HttpPost]
        public IActionResult NewsInfo(News news)
        {
            return View();
        }
    
    
    }
}
