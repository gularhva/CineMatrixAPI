using CineMatrixAPI.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.BookingDTOs
{
    public class BookingGetDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TicketId { get; set; }
        public DateTime BookingDateTime { get; set; }
    }
}
