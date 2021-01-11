using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmApp.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using EFDataAccessLibrary.Interfaces;
using EFDataAccessLibrary.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace FilmApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _appEnv;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public MoviesController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnv, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _appEnv = appEnv;
            _mapper = mapper;
            _config = config;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = _config.GetValue<int>("PageSize");

            IEnumerable<Movie> movies = await _unitOfWork.Movies.GetAllWithPoster(page, pageSize);

            int count = await _unitOfWork.Movies.Count();

            var viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Movies = movies
            };

            return View(viewModel);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            Movie movie = await _unitOfWork.Movies.GetWithPosterAndUser(id);
            if (movie is null) return NotFound();

            return View(movie);
        }

        [Authorize]
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        // POST: Movies/Create
        public async Task<IActionResult> Create(CreateMovieViewModel fvm)
        {
            if (ModelState.IsValid)
            {
                string login = User.Identity.Name;
                IdentityUser user = await _userManager.FindByNameAsync(login);

                if (user != null)
                {
                    string path = CreatePosterPath(fvm.UploadedFile);
                    await SavePosterAsync(fvm.UploadedFile, path);                   

                    fvm.Poster = new PosterViewModel
                    {
                        Name = WebUtility.HtmlEncode(fvm.UploadedFile.FileName),
                        Path = path,
                    };

                    fvm.User = user;

                    Movie movie = _mapper.Map<Movie>(fvm);

                    await _unitOfWork.Movies.Add(movie);
                    await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(fvm);
        }

        [Authorize]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return BadRequest();

            Movie movie = await _unitOfWork.Movies.GetWithPoster(id);
            if (movie is null) return NotFound();

            var fvm = _mapper.Map<EditMovieViewModel>(movie);

            return View(fvm);
        }

        // POST: Movies/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMovieViewModel fvm)
        {
            if (ModelState.IsValid)
            {
                Movie movie = await _unitOfWork.Movies.GetWithPosterAndUser(fvm.Id);

                var authorise = movie.User.UserName.Equals(User.Identity.Name);
                if (!authorise) return Forbid();

                movie.Title = fvm.Title;
                movie.Plot = fvm.Plot;
                movie.Year = fvm.Year;
                movie.Director = fvm.Director;
                movie.UpdatedAt = DateTime.Now;

                if (fvm.UploadedFile?.Length > 0)
                {
                    string path = CreatePosterPath(fvm.UploadedFile);
                    await SavePosterAsync(fvm.UploadedFile, path);

                    if(movie.Poster is null) movie.Poster = new Poster();
                    else DeleteFile(movie.Poster.Path);

                    movie.Poster.Name = WebUtility.HtmlEncode(fvm.UploadedFile.FileName);
                    movie.Poster.Path = path;
                }

                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            return View(fvm);
        }

        // GET: Movies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Movie movie = await _unitOfWork.Movies.Get(id);
            if (movie is null) return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0) return BadRequest();

            var movie = await _unitOfWork.Movies.GetWithPosterAndUser(id);
            if(movie is null) return NotFound();

            var authorise = movie.User.UserName.Equals(User.Identity.Name);
            if (!authorise) return Forbid();

            if(movie.Poster != null) DeleteFile(movie.Poster.Path);

            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        private string CreatePosterPath(IFormFile poster)
        {
            string dir = $"/Files/{ Guid.NewGuid() }/";

            string path = _appEnv.WebRootPath + dir;

            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);

            var filename = Path.GetFileNameWithoutExtension((Path.GetRandomFileName()));
            var ext = Path.GetExtension(poster.FileName);

            return dir + filename + ext;
        }

        private async Task SavePosterAsync(IFormFile poster, string path)
        {
            using var fileStream = new FileStream(_appEnv.WebRootPath + path, FileMode.Create);
            await poster.CopyToAsync(fileStream);
        }

        private void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(_appEnv.WebRootPath + path);
            if (file.Exists) file.Delete();
        }
    }
}
