using CineMatrixAPI.Entities;
using CineMatrixAPI.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Domain.Entities
{
    public class ShowTime:BaseEntity
    {
        public int MovieId { get; set; }
        public int BranchId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Branch Branch { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
