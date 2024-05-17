using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
