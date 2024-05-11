using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities.Common;

namespace CineMatrixAPI.Entities
{
    public class Review:BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Point { get; set; }
    }
}
