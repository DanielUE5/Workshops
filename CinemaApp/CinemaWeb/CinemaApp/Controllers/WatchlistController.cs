using CinemaApp.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            var watchlist = await watchlistService.GetUserWatchlistAsync(userId);
            return View(watchlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            bool movieExists = await watchlistService.MovieExistsAsync(id);

            if (!movieExists)
            {
                return NotFound();
            }

            await watchlistService.AddToWatchlistAsync(userId, id);

            return RedirectToAction("Index", "Movie");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string movieId)
        {
            string? userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }

            bool movieExists = await watchlistService.MovieExistsAsync(movieId);

            if (!movieExists)
            {
                return NotFound();
            }

            bool removed = await watchlistService.RemoveFromWatchlistAsync(userId, movieId);

            if (!removed)
            {
                return Forbid();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}