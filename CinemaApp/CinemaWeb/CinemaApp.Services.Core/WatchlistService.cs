using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly ApplicationDbContext dbContext;

        public WatchlistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            return await dbContext.UserMovies
                .Where(um => um.UserId == userId && !um.Movie.IsDeleted)
                .Select(um => new WatchlistViewModel
                {
                    MovieId = um.MovieId.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                    ImageUrl = um.Movie.ImageUrl ?? DefaultImageUrl
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsMovieInWatchlistAsync(string userId, string movieId)
        {
            bool isGuidValid = Guid.TryParse(movieId, out Guid movieGuid);

            if (!isGuidValid)
            {
                return false;
            }

            return await dbContext.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieGuid);
        }

        public async Task<bool> AddToWatchlistAsync(string userId, string movieId)
        {
            bool isGuidValid = Guid.TryParse(movieId, out Guid movieGuid);

            if (!isGuidValid)
            {
                return false;
            }

            bool movieExists = await dbContext.Movies
                .AnyAsync(m => m.Id == movieGuid && !m.IsDeleted);

            if (!movieExists)
            {
                return false;
            }

            bool alreadyExists = await dbContext.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieGuid);

            if (alreadyExists)
            {
                return false;
            }

            UserMovie userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieGuid
            };

            await dbContext.UserMovies.AddAsync(userMovie);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromWatchlistAsync(string userId, string movieId)
        {
            bool isGuidValid = Guid.TryParse(movieId, out Guid movieGuid);

            if (!isGuidValid)
            {
                return false;
            }

            UserMovie? userMovie = await dbContext.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieGuid);

            if (userMovie == null)
            {
                return false;
            }

            dbContext.UserMovies.Remove(userMovie);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MovieExistsAsync(string movieId)
        {
            bool isGuidValid = Guid.TryParse(movieId, out Guid movieGuid);

            if (!isGuidValid)
            {
                return false;
            }

            return await dbContext.Movies
                .AnyAsync(m => m.Id == movieGuid && !m.IsDeleted);
        }
    }
}