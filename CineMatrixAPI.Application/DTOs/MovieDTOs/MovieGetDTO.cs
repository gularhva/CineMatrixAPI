using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.MovieDTOs
{
    public class MovieGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public double IMDbRating { get; set; }
        public string Country { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
