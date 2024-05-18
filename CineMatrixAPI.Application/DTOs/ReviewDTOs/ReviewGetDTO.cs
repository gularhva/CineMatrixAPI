using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewGetDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public int Point { get; set; }
    }
}
