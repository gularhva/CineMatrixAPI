using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities.Common;

namespace CineMatrixAPI.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
    }
}

