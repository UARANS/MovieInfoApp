using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FilmApp.Models
{
    public class CreateMovieViewModel
    {
        [Required(ErrorMessage = "Введите название фильма")]
        [MaxLength(200)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание фильма")]
        [Display(Name = "Описание")]
        public string Plot { get; set; }

        [Required(ErrorMessage = "Заполните год выпуска фильма")]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Введите имя режиссёра фильма")]
        [MaxLength(200)]
        [Display(Name = "Режиссёр")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Загрузите постер фильма")]
        [Display(Name = "Постер")]
        public IFormFile UploadedFile { get; set; }

        public PosterViewModel Poster { get; set; }

        public IdentityUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
