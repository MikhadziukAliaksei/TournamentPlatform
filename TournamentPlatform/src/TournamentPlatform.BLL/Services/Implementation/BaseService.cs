using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(T item)
        {
            return _repository.AddAsync(item);
        }

        public Task UpdateAsync(T item)
        {
            return _repository.UpdateAsync(item);
        }

        public virtual Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate);
        }

        public virtual Task<T> GetByIdAsync(int id, Expression<Func<T, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate);
        }
    }
}