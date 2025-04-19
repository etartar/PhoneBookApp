using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PhoneBookApp.Core.Domain;
using System.Linq.Expressions;

namespace PhoneBookApp.Core.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext DbContext;

    protected RepositoryBase(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = DbContext.Set<TEntity>().AsQueryable();

        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include is not null)
        {
            queryable = include(queryable);
        }

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy is not null)
        {
            return await orderBy(queryable).ToListAsync(cancellationToken);
        }

        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = DbContext.Set<TEntity>().AsQueryable();

        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (include is not null)
        {
            queryable = include(queryable);
        }

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbContext.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Remove(entity);
    }
}
