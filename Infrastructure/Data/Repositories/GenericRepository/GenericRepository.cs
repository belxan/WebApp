using Domain.Interfaces.GenericRepository;
using Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories.GenericRepository;
public class GenericRepository<TEntity>(AppDbContext dbContext)
    : IGenericRepository<TEntity>
    where TEntity : class
{
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbContext.AddAsync(entity);
        return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
    {
        await dbContext.AddRangeAsync(entity);
        return entity;
    }

    public void Remove(TEntity entity)
    {
        dbContext.Remove(entity);
    }

    public void SoftDelete(TEntity entity)
    {
        var property = entity.GetType().GetProperty(nameof(BaseAuditableEntity.IsDeleted)) ?? throw new ArgumentException(
            $"""
             The property with type: {entity.GetType()} can not be SoftDeleted,
             because it doesn't contains {nameof(BaseAuditableEntity.IsDeleted)} property,
             and did not implemented {typeof(BaseAuditableEntity)}.
             """);

        if (((bool?)property.GetValue(entity)!).Value) throw new Exception("This entity was already deleted.");

        property.SetValue(entity, true);
        dbContext.Update(entity);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefaultAsync(filter)
            : await dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await dbContext.Set<TEntity>().IgnoreQueryFilters().ToListAsync()
                : await dbContext.Set<TEntity>().ToListAsync()
            : ignoreQueryFilters
                ? await dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().ToListAsync()
                : await dbContext.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<TEntity?> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity?> GetAsNoTrackingWithIdentityResolutionAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await dbContext.Set<TEntity>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(filter);
    }

    public TEntity Update(TEntity entity)
    {
        dbContext.Update(entity);
        return entity;
    }

    public List<TEntity> UpdateRange(List<TEntity> entity)
    {
        dbContext.UpdateRange(entity);
        return entity;
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().CountAsync(filter)
            : await dbContext.Set<TEntity>().CountAsync(filter);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AnyAsync(filter)
            : await dbContext.Set<TEntity>().AnyAsync(filter);
    }

    public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AllAsync(filter)
            : await dbContext.Set<TEntity>().AllAsync(filter);
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().SingleOrDefaultAsync(filter)
            : await dbContext.Set<TEntity>().SingleOrDefaultAsync(filter);
    }

    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().SingleAsync(filter)
            : await dbContext.Set<TEntity>().SingleAsync(filter);
    }

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().FirstAsync(filter)
            : await dbContext.Set<TEntity>().FirstAsync(filter);
    }
}
