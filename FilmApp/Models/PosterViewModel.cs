using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmApp.Models
{
    public class PosterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Path { get; set; }
    }
}
