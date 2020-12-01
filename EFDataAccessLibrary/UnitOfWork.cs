using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLibrary.Interfaces;
using EFDataAccessLibrary.Repositories;

namespace EFDataAccessLibrary
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlutoContext _context;
        public IMovieRepository Movies { get; private set; }

        public UnitOfWork(PlutoContext context, IMovieRepository movies)
        {
            _context = context;
            Movies = movies;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
