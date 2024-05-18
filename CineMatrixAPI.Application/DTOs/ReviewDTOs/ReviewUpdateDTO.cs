using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.ReviewDTOs
{
    public class ReviewUpdateDTO
    {
        public string ReviewText { get; set; } = string.Empty;
        public int Point { get; set; }
    }
}
