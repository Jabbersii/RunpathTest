using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Data()
        {
            var photos = new[]
            {
                new PhotoViewModel()
                {
                    PhotoTitle = "accusamus beatae ad facilis cum similique qui sunt",
                    AlbumName = "quidem molestiae enim",
                    Url = new Uri("https://via.placeholder.com/600/92c952"),
                    ThumbnailUrl = new Uri("https://via.placeholder.com/150/92c952")
                },
                new PhotoViewModel()
                {
                    PhotoTitle = "accusamus beatae ad facilis cum similique qui sunt",
                    AlbumName = "quidem molestiae enim",
                    Url = new Uri("https://via.placeholder.com/600/92c952"),
                    ThumbnailUrl = new Uri("https://via.placeholder.com/150/92c952")
                }
            };

            return Json(photos);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
