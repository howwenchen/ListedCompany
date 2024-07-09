using Microsoft.EntityFrameworkCore.Storage;

namespace ListedCompany.Services.Repository.UnitOfWork;

/// <summary>
/// Interface for Unit Of Work
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// 儲存所有異動。
    /// </summary>
    Task SaveAsync();

    /// <summary>
    /// 使用Transaction
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// 取得某一個Entity的Repository
    /// 如果沒有取過，會initialize一個
    /// 如果有就取得之前initialize的那個
    /// </summary>
    /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
    /// <returns>Entity的Repository</returns>
    IGenericRepository<T> Repository<T>() where T : class;
}
