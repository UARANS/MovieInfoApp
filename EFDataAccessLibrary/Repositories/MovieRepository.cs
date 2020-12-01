using System;
using System.Collections.Generic;
using System.Text;
using EFDataAccessLibrary.Interfaces;
using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace EFDataAccessLibrary.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(PlutoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Movie>> GetAllWithPoster(int pageIndex, int pageSize)
        {
            return await PlutoContext.Movies
                .Include(x => x.Poster)
                .OrderByDescending(x => x.UpdatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Movie> GetWithPoster(int id)
        {
            return await PlutoContext.Movies
                .Include(x => x.Poster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> GetWithPosterAndUser(int id)
        {
            return await PlutoContext.Movies
                .Include(x => x.Poster)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public PlutoContext PlutoContext
        {
            get { return Context as PlutoContext; }
        }
    }
}
