using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Contexts;
using CineMatrixAPI.Entities.Common;
using CineMatrixAPI.Persistance.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _repositories = new Dictionary<Type, object>();

        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            GenericRepository<TEntity> genericRepository = new GenericRepository<TEntity>(_appDbContext);
            _repositories.Add(typeof(TEntity), genericRepository);
            return genericRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
