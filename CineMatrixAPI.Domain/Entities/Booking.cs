using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineMatrixAPI.Entities
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public virtual AppUser? User { get; set; }
        public int TicketId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public virtual Ticket? Ticket { get; set; }
        
    }
}
