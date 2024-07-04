using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ListedCompany.Services.Repository.UnitOfWork;

/// <summary>
/// 實作Entity Framework Unit Of Work的class
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed;
    private Hashtable _repositories;

    /// <summary>
    /// 設定此Unit of work(UOF)的Context和ServiceProvider
    /// </summary>
    /// <param name="context">設定UOF的context</param>
    /// <param name="serviceProvider">ServiceProvider 用來解析Repository實例</param>
    public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 儲存所有異動
    /// </summary>
    public void Save()
    {
        _context.SaveChanges();
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
        }

        _disposed = true;
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
        //if null new a hashtable
        _repositories ??= new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = _serviceProvider.GetService(typeof(IGenericRepository<T>));
            if (repositoryInstance == null)
            {
                var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
                repositoryInstance = Activator.CreateInstance(repositoryType, _context);
            }

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }
}