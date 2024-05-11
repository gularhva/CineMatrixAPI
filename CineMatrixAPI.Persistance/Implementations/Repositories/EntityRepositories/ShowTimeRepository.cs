using CineMatrixAPI.Application.Abstractions.IRepositories.IEntityRepositories;
using CineMatrixAPI.Contexts;
using CineMatrixAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Repositories.EntityRepositories
{
    public class ShowTimeRepository : GenericRepository<ShowTime>, IShowTimeRepository
    {
        public ShowTimeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
