using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using FilmApp.Validation;

namespace FilmApp.Models
{
    public class EditMovieViewModel
    {
        public int Id { get; set; }

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

        [Display(Name = "Постер")]
        [MaxPosterSize(2097152, ErrorMessage = "Загруженный файл слишком большой")]
        [PermittedPosterExtensions(new string[] { ".jpg", ".jpeg", ".png" }, 
            ErrorMessage = "Неразрешенный формат файла")]
        public IFormFile UploadedFile { get; set; }
                        
        public PosterViewModel Poster { get; set; }
    }
}
