using CineMatrixAPI.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewCreateDTO
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public string ReviewText { get; set; }
        public int Point { get; set; }
    }
}
