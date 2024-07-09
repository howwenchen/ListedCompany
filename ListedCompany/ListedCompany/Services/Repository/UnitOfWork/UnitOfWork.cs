using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace ListedCompany.Services.Repository.UnitOfWork;

/// <summary>
/// 實作Entity Framework Unit Of Work的class
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed;
    private ConcurrentDictionary<string, object> _repositories;

    /// <summary>
    /// 設定此Unit of work(UOF)的Context和ServiceProvider
    /// </summary>
    /// <param name="context">設定UOF的context</param>
    /// <param name="serviceProvider">ServiceProvider 用來解析Repository實例</param>
    public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _repositories = new ConcurrentDictionary<string, object>();
    }

    /// <summary>
    /// 儲存所有異動
    /// </summary>
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 使用Transaction
    /// </summary>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// 清除此Class的資源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 清除此Class的資源
    /// </summary>
    /// <param name="disposing">是否在清理中？</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// 取得某一個Entity的Repository
    /// 如果沒有取過，會initialize一個
    /// 如果有就取得之前initialize的Repository
    /// </summary>
    /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
    /// <returns>Entity的Repository</returns>
    public IGenericRepository<T> Repository<T>() where T : class
    {
        var typeName = typeof(T).Name;

        if (!_repositories.ContainsKey(typeName))
        {
            var repositoryInstance = _serviceProvider.GetService(typeof(IGenericRepository<T>));
            if (repositoryInstance == null)
            {
                var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
                repositoryInstance = Activator.CreateInstance(repositoryType, _context, _serviceProvider);
            }
            _repositories.TryAdd(typeName, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[typeName];
    }
}
