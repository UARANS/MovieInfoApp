using System;
using System.Collections.Generic;
using System.Text;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLibrary
{
    public class PlutoContext : IdentityDbContext
    {
        public PlutoContext(DbContextOptions<PlutoContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Poster> Posters { get; set; }
    }

}
