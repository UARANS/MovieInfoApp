using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFDataAccessLibrary.Models;
using FilmApp.Models;

namespace FilmApp.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Movie, EditMovieViewModel>();
            CreateMap<Poster, PosterViewModel>().ReverseMap();
            CreateMap<CreateMovieViewModel, Movie>();
        }
    }
}
