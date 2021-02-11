using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TournamentPlatform.DAL.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> includes = null);
        Task<T> GetByIdAsync(int id, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> includes = null);
    }
}