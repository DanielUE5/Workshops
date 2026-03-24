using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static CinemaApp.GCommon.ApplicationConstants;
using static CinemaApp.Data.Common.EntityConstants.Movie;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext dbContext;

        public MovieService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync(string? userId = null)
        {
            HashSet<string> watchlistMovieIds = new HashSet<string>();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                List<string> movieIds = await dbContext
                    .UserMovies
                    .Where(um => um.UserId == userId)
                    .Select(um => um.MovieId.ToString())
                    .ToListAsync();

                watchlistMovieIds = movieIds.ToHashSet();
            }

            return await dbContext
                .Movies
                .Where(m => !m.IsDeleted)
                .AsNoTracking()
                .Select(movie => new AllMoviesIndexViewModel
                {
                    Id = movie.Id.ToString(),
                    Title = movie.Title,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                    Director = movie.Director,
                    Duration = movie.Duration.ToString(),
                    ImageUrl = movie.ImageUrl ?? DefaultImageUrl,
                    IsInWatchlist = watchlistMovieIds.Contains(movie.Id.ToString())
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Genre)
                .ThenBy(m => m.Director)
                .ToListAsync();
        }

        public async Task AddAsync(MovieFormViewModel model, string ownerId)
        {
            Movie movie = new Movie
            {
                Title = model.Title,
                Genre = model.Genre,
                Director = model.Director,
                Description = model.Description,
                Duration = model.Duration,
                ReleaseDate = DateOnly.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture),
                ImageUrl = model.ImageUrl,
                OwnerId = ownerId
            };

            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task<MovieDetailsViewModel?> GetByIdAsync(string id)
        {
            Movie? movie = await dbContext
                .Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id && !m.IsDeleted);

            if (movie == null)
            {
                return null;
            }

            return new MovieDetailsViewModel
            {
                Id = movie.Id.ToString(),
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Description = movie.Description,
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                ImageUrl = movie.ImageUrl ?? DefaultImageUrl
            };
        }

        public async Task<MovieFormViewModel?> GetForEditByIdAsync(string id)
        {
            return await dbContext
                .Movies
                .Where(m => m.Id.ToString() == id && !m.IsDeleted)
                .Select(m => new MovieFormViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description,
                    Duration = m.Duration,
                    ReleaseDate = m.ReleaseDate.ToString(ReleaseDateFormat, CultureInfo.InvariantCulture),
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditAsync(string id, MovieFormViewModel model)
        {
            Movie? movie = await dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id.ToString() == id && !m.IsDeleted);

            if (movie == null)
            {
                return;
            }

            movie.Title = model.Title;
            movie.Genre = model.Genre;
            movie.Director = model.Director;
            movie.Description = model.Description;
            movie.Duration = model.Duration;
            movie.ReleaseDate = DateOnly.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture);
            movie.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            Movie? movie = await dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null && !movie.IsDeleted)
            {
                movie.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task HardDeleteAsync(string id)
        {
            Movie? movie = await dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                dbContext.Movies.Remove(movie);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsOwnerAsync(string movieId, string userId)
        {
            return await dbContext.Movies
                .AnyAsync(m => m.Id.ToString() == movieId && m.OwnerId == userId && !m.IsDeleted);
        }
    }
}