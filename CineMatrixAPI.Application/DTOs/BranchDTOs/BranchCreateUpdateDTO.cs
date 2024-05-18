using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.BranchDTOs
{
    public class BranchCreateUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

    }
}
