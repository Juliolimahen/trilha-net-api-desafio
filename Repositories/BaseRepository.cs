using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Interfaces;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly OrganizadorContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(OrganizadorContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> RemoveAsync(int? id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Entidade com o ID {id} não foi encontrada.");
            }

            var productRemoved = _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return productRemoved.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new Exception($"Entidade com o ID {entity.Id} não foi encontrada.");
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return existingEntity;
        }
        
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}