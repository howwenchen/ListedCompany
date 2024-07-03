using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ListedCompany.Services.Repository.UnitOfWork;

/// <summary>
/// UnitOfWork
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    ///
    /// </summary>
    private bool disposed = false;
    public IGenericRepository<MON_REV> MON_REVRepository { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="MON_REVRepository">The MON_REV repository.</param>
    public UnitOfWork(
    DbContext context,
        IGenericRepository<MON_REV> MON_REVRepository)
    {
        this.Context = context;
        this.MON_REVRepository = MON_REVRepository;
    }

    /// <summary>
    /// </summary>
    

    /// <summary>
    /// Context
    /// </summary>
    public DbContext Context { get; private set; }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// SaveChange
    /// </summary>
    /// <returns></returns>
    public async Task<int> SaveChangeAsync()
    {
        return await this.Context.SaveChangesAsync();
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.Context.Dispose();
                this.Context = null;
            }
        }
        this.disposed = true;
    }
}