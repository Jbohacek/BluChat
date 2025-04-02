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
        public AppDbContext Context;
        internal DbSet<T> _dbSet;

        protected Repo(AppDbContext context)
        {
            Context = context;
            _dbSet = context.Set<T>();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filterExpression) => _dbSet.First(filterExpression);

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> filterExpression) => _dbSet.Where(filterExpression).ToList();

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
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
