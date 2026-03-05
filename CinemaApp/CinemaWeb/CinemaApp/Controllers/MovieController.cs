using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new List<AllMoviesIndexViewModel>());
        }
    }
}
