using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models;

namespace EFDataAccessLibrary.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetWithPoster(int id);
        Task<Movie> GetWithPosterAndUser(int id);
        Task<IEnumerable<Movie>> GetAllWithPoster(int pageIndex, int pageSize);
    }
}
