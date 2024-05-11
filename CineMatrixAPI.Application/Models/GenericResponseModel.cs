using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Models
{
    public class GenericResponseModel<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
