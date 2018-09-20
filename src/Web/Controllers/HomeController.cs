using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestEase;
using Web.Exceptions;
using Web.Handlers;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Data()
        {
            try
            {
                var photos = await this.mediator.Send(new GetPhotos());
                return Json(photos);
            }
            catch(RunpathAlbumWebException)
            {
                return StatusCode(500);
            }
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
