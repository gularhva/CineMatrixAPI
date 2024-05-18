using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities.Common;

namespace CineMatrixAPI.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<ShowTime>? ShowTimes { get; set; }
    }
}

