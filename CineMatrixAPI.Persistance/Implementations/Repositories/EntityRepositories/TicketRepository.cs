using CineMatrixAPI.Application.Abstractions.IRepositories.IEntityRepositories;
using CineMatrixAPI.Contexts;
using CineMatrixAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Repositories.EntityRepositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
