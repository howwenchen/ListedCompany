using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ListedCompany.Services.Repository;

/// <summary>
///A generic repository class for handling CRUD operations with Entity Framework
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="Repository.Interface.IGenericRepository{TEntity}" />
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// The DbContext instance.
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public GenericRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity">實體</param>
    /// <exception cref="ArgumentNullException">Thrown when the entity is null</exception>
    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// 取得全部
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// 取得單筆
    /// </summary>
    /// <param name="predicate">查詢條件</param>
    /// <returns></returns>
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// 刪除
    /// </summary>
    /// <param name="entity">實體</param>
    /// <exception cref="ArgumentNullException">entity</exception>
    public void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">實體</param>
    /// <exception cref="ArgumentNullException">entity</exception>
    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Modified;
    }
}