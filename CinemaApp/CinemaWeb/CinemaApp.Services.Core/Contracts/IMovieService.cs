using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync(string? userId = null);

        Task AddAsync(MovieFormViewModel model, string ownerId);

        Task<MovieDetailsViewModel?> GetByIdAsync(string id);

        Task<MovieFormViewModel?> GetForEditByIdAsync(string id);

        Task EditAsync(string id, MovieFormViewModel model);

        Task SoftDeleteAsync(string id);

        Task HardDeleteAsync(string id);

        Task<bool> IsOwnerAsync(string movieId, string userId);
    }
}