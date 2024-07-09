﻿using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ExpenseSharingContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ExpenseSharingContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> Entities => _context.Set<T>();

        public void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task DeleteAsync(object id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate));
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IEnumerable<T> Get(int index, int pageSize)
        {
            return _dbSet.Skip(index * pageSize).Take(pageSize).ToList();
        }



        public List<T> GetAllAsync()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IQueryable<T>> GetAllQueryableAsync()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task<IEnumerable<T>> GetAsync(int index, int pageSize)
        {
            return await _dbSet.Skip(index * pageSize).Take(pageSize).ToListAsync();
        }

        public T GetById(object id)
        {

            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T obj)
        {
            _dbSet.Add(obj);
        }

        public async Task InsertAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public void InsertRange(List<T> obj)
        {
            _dbSet.AddRange(obj);
        }
        public async Task InsertCollection(ICollection<T> collection)
        {
            await _dbSet.AddRangeAsync(collection);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}