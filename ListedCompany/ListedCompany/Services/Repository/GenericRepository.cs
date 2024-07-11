using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using Dapper;
using ListedCompany.Services.DatabaseHelper;
using ListedCompany.Models;

namespace ListedCompany.Services.Repository;

/// <summary>
/// A generic repository class for handling CRUD operations with Entity Framework
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly db_aaa9ad_project20240703Context _context;
    private readonly IDatabaseHelper _databaseHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    /// <param name="databaseHelper">The database helper instance.</param>
    public GenericRepository(db_aaa9ad_project20240703Context context, IDatabaseHelper databaseHelper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _databaseHelper = databaseHelper ?? throw new ArgumentNullException(nameof(databaseHelper));
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity">實體</param>
    /// <exception cref="ArgumentNullException">Thrown when the entity is null</exception>
    public async Task AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 取得符合條件的IEnumerable
    /// </summary>
    /// <param name="predicate">查詢條件</param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
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
    public async Task RemoveAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">實體</param>
    /// <exception cref="ArgumentNullException">entity</exception>
    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 執行自訂SQL語法並回傳資料
    /// </summary>
    /// <typeparam name="T">回傳資料的型別</typeparam>
    /// <param name="sql">自訂SQL語法</param>
    /// <param name="parameters">SQL語法的參數</param>
    /// <returns>SQL語法執行結果</returns>
    public async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object parameters = null)
    {
        using (var connection = _databaseHelper.GetConnection())
        {
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }

    /// <summary>
    /// 執行不回傳資料的自訂SQL語法
    /// </summary>
    /// <param name="sql">自訂SQL語法</param>
    /// <param name="parameters">SQL參數</param>
    /// <returns>Task</returns>
    public async Task ExecuteNonQuery(string sql, object parameters = null)
    {
        using (var connection = _databaseHelper.GetConnection())
        {
            await connection.ExecuteAsync(sql, parameters);
        }
    }

    /// <summary>
    /// 執行預存程序並回傳資料
    /// </summary>
    /// <typeparam name="T">回傳資料的型別</typeparam>
    /// <param name="storedProcedure">預存程序名稱</param>
    /// <param name="parameters">預存程序參數</param>
    /// <returns>預存程序執行結果</returns>
    public async Task<IEnumerable<T>> ExecuteSPQuery<T>(string storedProcedure, object parameters = null)
    {
        using (var connection = _databaseHelper.GetConnection())
        {
            return await connection.QueryAsync<T>(storedProcedure, parameters ?? new { }, commandType: CommandType.StoredProcedure);
        }
    }

    /// <summary>
    /// 執行不回傳資料的預存程序
    /// </summary>
    /// <param name="storedProcedure">預存程序名稱</param>
    /// <param name="parameters">預存程序參數</param>
    /// <returns>Task</returns>
    public async Task ExecuteSP(string storedProcedure, object parameters = null)
    {
        using (var connection = _databaseHelper.GetConnection())
        {
            await connection.ExecuteAsync(storedProcedure, parameters ?? new { }, commandType: CommandType.StoredProcedure);
        }
    }

}
