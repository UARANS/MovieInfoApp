using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EFDataAccessLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(200)]
        public string Director { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        public Poster Poster { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
