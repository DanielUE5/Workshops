using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = await movieService
                .GetAllMoviesOrderedByTitleAsync();

            return View(allMoviesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }
            await movieService.AddAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            MovieDetailsViewModel movieDetailsViewModel = await movieService.GetByIdAsync(id);

            if (movieDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(movieDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            MovieFormViewModel movieFormViewModel = await movieService.GetForEditByIdAsync(id);
            if (movieFormViewModel == null)
            {
                return NotFound();
            }
            return View(movieFormViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, MovieFormViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await movieService.EditAsync(id, inputModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            MovieDetailsViewModel movieDetailsViewModel = await movieService.GetByIdAsync(id);

            if (movieDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(movieDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, MovieDetailsViewModel inputModel)
        {
            await movieService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}