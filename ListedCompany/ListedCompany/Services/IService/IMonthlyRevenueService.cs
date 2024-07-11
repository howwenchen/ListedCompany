using ListedCompany.Models;
using ListedCompany.ViewModels;

namespace ListedCompany.Services.IService;

/// <summary>
/// 定義月營收服務的介面
/// </summary>
public interface IMonthlyRevenueService : IService<MonRevenue>
{
    /// <summary>
    /// 使用篩選條件查詢資料
    /// </summary>
    /// <param name="companyId">公司代號</param>
    /// <returns>符合條件的月營收資料</returns>
    Task<IEnumerable<MonRevenueViewModel>> QueryMonthlyRevenuesFilterAsync(string companyId);

    /// <summary>
    /// 使用預存程序查詢資料
    /// </summary>
    /// <returns>月營收資料</returns>
    Task<IEnumerable<MonRevenueViewModel>> QueryMonthlyRevenuesAsync();

    /// <summary>
    /// 使用預存程序新增資料
    /// </summary>
    /// <param name="revenueViewModel">月營收資料的 ViewModel</param>
    /// <returns>是否新增成功</returns>
    Task<bool> AddMonthlyRevenueAsync(MonRevenueViewModel revenueViewModel);
}
