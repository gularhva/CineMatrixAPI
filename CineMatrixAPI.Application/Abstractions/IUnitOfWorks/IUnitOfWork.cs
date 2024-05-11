using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        public Task<int> SaveAsync();
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
