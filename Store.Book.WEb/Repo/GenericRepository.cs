﻿using Store.Book.WEb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Book.WEb
{
    public class GenericRepository<T> where T : class
    {
        internal ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null 
                ? await orderBy(query).ToListAsync() 
                : await query.ToListAsync();
        }

        public virtual async Task<T> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            await Delete(entityToDelete);
        }

        public virtual async Task Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public virtual async Task Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
