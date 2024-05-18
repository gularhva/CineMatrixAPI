using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities.Common;

namespace CineMatrixAPI.Entities
{
    public class Movie:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public double IMDbRating { get; set; }
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ShowTime>? ShowTimes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
