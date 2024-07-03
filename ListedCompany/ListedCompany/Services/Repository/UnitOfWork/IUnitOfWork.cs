using Microsoft.EntityFrameworkCore;

namespace ListedCompany.Services.Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// DB Context
    /// </summary>
    DbContext Context { get; }

    /// <summary>
    /// 儲存所有異動.
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangeAsync();
}
