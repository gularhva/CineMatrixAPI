using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.MovieDTOs
{
    public class MovieCreateUpdateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public double IMDbRating { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}
