using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BluChat.Core.Data
{
    public abstract class Repo<T> where T : class , ITable
    {
        public SqlLiteContext Context;
        internal DbSet<T> _dbSet;

        protected Repo(SqlLiteContext context)
        {
            Context = context;
            _dbSet = context.Set<T>();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filterExpression) => _dbSet.First(filterExpression);

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> filterExpression) => _dbSet.Where(filterExpression).ToList();

        public IEnumerable<T> GetAll(params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public virtual void Add(T item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = new Guid();
            }
            Context.Add(item);
        }

        public void Remove(T item)
        {
            Context.Remove(item);
        }

        public void Update(T item)
        {
            Context.Update(item);
        }

    }
}
