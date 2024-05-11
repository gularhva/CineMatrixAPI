using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineMatrixAPI.Entities
{
    public class Ticket:BaseEntity
    {
        public int SeatNumber { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public int ShowTimeId { get; set; }
        public virtual ShowTime ShowTime { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
