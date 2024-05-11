﻿using CineMatrixAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.DTOs.ShowTimeDTOs
{
    public class ShowTimeCreateDTO
    {
        public int MovieId { get; set; }
        public int BranchId { get; set; }
        public DateTime DateTime { get; set; }
    }
}