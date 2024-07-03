using System.Linq.Expressions;

namespace ListedCompany.Services.Repository;

/// <summary>
/// Interface Repository
/// </summary>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 新增一筆資料
    /// </summary>
    /// <param name="entity">要新增到的Entity</param>
    void Add(TEntity entity);

    /// <summary>
    /// 取得全部
    /// </summary>
    /// <returns>Entity全部筆數的IEnumerable</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// 取得單筆
    /// </summary>
    /// <param name="predicate">要取得的Where條件</param>
    /// <returns>取得第一筆符合條件的內容</returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity</param>
    void Remove(TEntity entity);

    /// <summary>
    /// 更新一筆資料的內容
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    void Update(TEntity entity);
}