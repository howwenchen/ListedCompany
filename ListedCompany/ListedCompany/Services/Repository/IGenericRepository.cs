using System.Linq.Expressions;

namespace ListedCompany.Services.Repository
{
    /// <summary>
    /// Interface Repository
    /// </summary>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 新增一筆資料
        /// </summary>
        /// <param name="entity">要新增的Entity</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 取得符合條件的IEnumerable
        /// </summary>
        /// <param name="predicate">要取得的Where條件</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

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
        Task RemoveAsync(TEntity entity);

        /// <summary>
        /// 更新一筆資料的內容
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 執行預存程序並回傳資料。
        /// </summary>
        /// <typeparam name="T">回傳資料的型別</typeparam>
        /// <param name="storedProcedure">預存程序名稱</param>
        /// <param name="parameters">預存程序參數</param>
        /// <returns>預存程序執行結果</returns>
        Task<IEnumerable<T>> ExecuteSPQuery<T>(string storedProcedure, object parameters = null);

        /// <summary>
        /// 執行不回傳資料的預存程序。
        /// </summary>
        /// <param name="storedProcedure">預存程序名稱</param>
        /// <param name="parameters">預存程序參數</param>
        /// <returns>Task</returns>
        Task ExecuteSP(string storedProcedure, object parameters = null);
    }
}
