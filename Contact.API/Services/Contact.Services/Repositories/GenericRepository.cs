using Contact.Application.Repositories;
using Contact.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ContactDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ContactDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            ;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);

        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();

        }

        public IQueryable<T> GetAllByColorFilter(string color)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            return await _dbSet.FirstOrDefaultAsync(method);

        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return _dbSet.Where(method);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<T> entites)
        {
            _dbSet.RemoveRange(entites);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }
    }
}