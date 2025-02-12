using APISistemaGestaoViagens.Data;

namespace APISistemaGestaoViagens.Repository.Implementations;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Repository.Interfaces;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>> include = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (include != null)
            query = include(query);

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(
        int id,
        Func<IQueryable<T>, IQueryable<T>> include = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (include != null)
            query = include(query);
        
        var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
            .FirstOrDefault()?.Name;

        if (string.IsNullOrEmpty(keyName))
            throw new InvalidOperationException("Chave primária não encontrada.");

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
    }


    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
    }
}