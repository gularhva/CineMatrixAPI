using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineMatrixAPI.Entities
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int TicketId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public virtual Ticket Ticket { get; set; }
        //user-booking one to many bir userin bir cox bookingi ola biler, bir booking yalniz 1 usere aid ola biler.
        //ticket-booking 1 bookingde 1 bilet ola biler,1 bilet 1 bookingde ola biler.
    }
}
