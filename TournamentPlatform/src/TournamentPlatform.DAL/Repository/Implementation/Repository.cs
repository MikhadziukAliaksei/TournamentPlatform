using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.DAL.Context;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DAL.Repository.RepositoryHelper;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DAL.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly TournamentContext _context;

        public Repository(TournamentContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            var entry = await _context.Set<T>().FirstAsync(e => e.Id == item.Id);
            _context.Entry(entry).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.FindAsync<T>(id);

            if (item == null)
            {
                return;
            }

            _context.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public Task<T> GetByIdAsync(int id, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}