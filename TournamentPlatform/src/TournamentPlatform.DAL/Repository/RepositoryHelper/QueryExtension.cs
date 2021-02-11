using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DAL.Repository.RepositoryHelper
{
    public static class QueryExtension
    {
        public static IQueryable<T> Include<T>(this DbSet<T> dbSet, Expression<Func<T, object>>[] includes) where T : class, IEntity
        {
            var query = dbSet.AsQueryable();

            return includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}