using CinemaApp.Web.ViewModels.Watchlist;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        Task<bool> IsMovieInWatchlistAsync(string userId, string movieId);

        Task<bool> AddToWatchlistAsync(string userId, string movieId);

        Task<bool> RemoveFromWatchlistAsync(string userId, string movieId);

        Task<bool> MovieExistsAsync(string movieId);
    }
}
