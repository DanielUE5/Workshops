using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IWatchlistService watchlistService;

        public MovieController(
            IMovieService movieService,
            IWatchlistService watchlistService)
        {
            this.movieService = movieService;
            this.watchlistService = watchlistService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = IsUserAuthenticated()
                ? GetUserId()
                : null;

            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel =
                await movieService.GetAllMoviesOrderedByTitleAsync(userId);

            return View(allMoviesViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            MovieDetailsViewModel? movieDetailsViewModel = await movieService.GetByIdAsync(id);

            if (movieDetailsViewModel == null)
            {
                return NotFound();
            }

            ViewBag.IsOwner = false;
            ViewBag.IsInWatchlist = false;

            if (IsUserAuthenticated())
            {
                string? userId = GetUserId();

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    ViewBag.IsOwner = await movieService.IsOwnerAsync(id, userId);
                    ViewBag.IsInWatchlist = await watchlistService.IsMovieInWatchlistAsync(userId, id);
                }
            }

            return View(movieDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            await movieService.AddAsync(inputModel, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            MovieFormViewModel? movieFormViewModel = await movieService.GetForEditByIdAsync(id);

            if (movieFormViewModel == null)
            {
                return NotFound();
            }

            bool isOwner = await movieService.IsOwnerAsync(id, userId);

            if (!isOwner)
            {
                return Forbid();
            }

            return View(movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MovieFormViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            MovieFormViewModel? movieFormViewModel = await movieService.GetForEditByIdAsync(id);

            if (movieFormViewModel == null)
            {
                return NotFound();
            }

            bool isOwner = await movieService.IsOwnerAsync(id, userId);

            if (!isOwner)
            {
                return Forbid();
            }

            await movieService.EditAsync(id, inputModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            MovieDetailsViewModel? movieDetailsViewModel = await movieService.GetByIdAsync(id);

            if (movieDetailsViewModel == null)
            {
                return NotFound();
            }

            bool isOwner = await movieService.IsOwnerAsync(id, userId);

            if (!isOwner)
            {
                return Forbid();
            }

            return View(movieDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, MovieDetailsViewModel inputModel)
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            MovieDetailsViewModel? movieDetailsViewModel = await movieService.GetByIdAsync(id);

            if (movieDetailsViewModel == null)
            {
                return NotFound();
            }

            bool isOwner = await movieService.IsOwnerAsync(id, userId);

            if (!isOwner)
            {
                return Forbid();
            }

            await movieService.SoftDeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}