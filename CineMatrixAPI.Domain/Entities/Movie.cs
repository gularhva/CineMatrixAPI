using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities.Common;

namespace CineMatrixAPI.Entities
{
    public class Movie:BaseEntity
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
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
