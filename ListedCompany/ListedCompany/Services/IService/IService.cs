using System.Linq.Expressions;

namespace ListedCompany.Services.IService;

/// <summary>
/// Service服務層內容的Interface
/// </summary>
/// <typeparam name="T">主要要儲存的Entity Type</typeparam>
public interface IService<T> where T : class
{
    /// <summary>
    /// 取得符合條件的Entity並且轉成對應的ViewModel
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
    /// <param name="predicate">過濾邏輯</param>
    /// <returns>取得轉換過的ViewModel List</returns>
    Task<List<TViewModel>> GetListToViewModelAsync<TViewModel>(Expression<Func<T, bool>>? predicate);

    /// <summary>
    /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
    /// <param name="viewModel">ViewModel的Reference</param>
    /// <returns>是否儲存成功</returns>
    Task<bool> CreateViewModelToDatabaseAsync<TViewModel>(TViewModel viewModel);

    /// <summary>
    /// 依照某一個ViewModel的值，更新對應的Entity
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
    /// <param name="viewModel">ViewModel的值</param>
    /// <param name="predicate">過濾條件 - 要被更新的那一筆過濾條件</param>
    /// <returns>是否更新成功</returns>
    Task<bool> UpdateViewModelToDatabaseAsync<TViewModel>(TViewModel viewModel,
        Expression<Func<T, bool>> predicate);

    /// <summary>
    /// 刪除某一筆Entity
    /// </summary>
    /// <param name="predicate">過濾出要被刪除的Entity條件</param>
    /// <returns>是否刪除成功</returns>
    Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);


    /// <summary>
    /// 執行預存程序並返回查詢結果
    /// </summary>
    /// <typeparam name="TResult">回傳資料的型別</typeparam>
    /// <param name="storedProcedure">預存程序名稱</param>
    /// <param name="parameters">預存程序參數</param>
    /// <returns>預存程序執行結果</returns>
    Task<IEnumerable<TResult>> QuerySPFromsql<TResult>(string storedProcedure, object parameters = null);

    /// <summary>
    /// 執行預存程序，不返回查詢結果
    /// </summary>
    /// <param name="storedProcedure">預存程序名稱</param>
    /// <param name="parameters">預存程序參數</param>
    /// <returns>是否執行成功</returns>
    Task<bool> ExecuteSPFromsql(string storedProcedure, object parameters = null);
}
