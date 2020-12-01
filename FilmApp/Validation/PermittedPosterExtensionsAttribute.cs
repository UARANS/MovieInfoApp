using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FilmApp.Validation
{
    public class PermittedPosterExtensionsAttribute : ValidationAttribute
    {
        public string[] PermittedExtensions { get; }

        public PermittedPosterExtensionsAttribute(string[] extensions)
        {
            PermittedExtensions = extensions;
        }

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();

                if (string.IsNullOrEmpty(ext) || !PermittedExtensions.Contains(ext)) return false;
            }

            return true;
        }
    }
}