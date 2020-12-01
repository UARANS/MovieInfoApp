using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        Task<int> Complete();
    }
}
