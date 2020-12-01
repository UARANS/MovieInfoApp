using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FilmApp.Validation
{
    public class MaxPosterSizeAttribute : ValidationAttribute
    {
        public long PosterSizeLimit { get; }

        public MaxPosterSizeAttribute(long posterSizeLimit)
        {
            PosterSizeLimit = posterSizeLimit;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if(file?.Length > PosterSizeLimit) return false;

            return true;
        }
    }
}
