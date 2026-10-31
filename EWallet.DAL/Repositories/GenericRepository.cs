using System.Linq.Expressions;
using EWallet.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> Set;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        Set = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        return await Set.FindAsync(new[] { id });
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = Set;
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Set.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }

    public void Remove(T entity)
    {
        Set.Remove(entity);
    }
}
