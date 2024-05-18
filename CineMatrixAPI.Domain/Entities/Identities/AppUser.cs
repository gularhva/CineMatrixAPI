using CineMatrixAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Domain.Entities.Identities
{
    public class AppUser:IdentityUser<string>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;    
        public DateTime Birthday { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
