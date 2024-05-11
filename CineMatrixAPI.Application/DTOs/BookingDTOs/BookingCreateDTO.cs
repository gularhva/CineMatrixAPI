using CineMatrixAPI.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.BookingDTOs
{
    public class BookingCreateDTO
    {
        public string UserId { get; set; }
        public int TicketId { get; set; }
    }
}
