﻿using System.Linq.Expressions;
using Application.Interface;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public virtual T Add(T entity)
    {
        return _context.Add(entity).Entity;
    }

    public virtual Task<T> GetByKey(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public  virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T> FindSingle(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .FirstOrDefaultAsync(cancellationToken);        
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public virtual int AddRange(List<T> entities)
    {
        _context.AddRange(entities);
        return entities.Count;
    }
}