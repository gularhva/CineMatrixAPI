using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.TicketDTOs
{
    public class TicketUpdateDTO
    {
        public int SeatNumber { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public int ShowTimeId { get; set; }
    }
}
