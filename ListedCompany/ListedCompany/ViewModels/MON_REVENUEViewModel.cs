namespace ListedCompany.ViewModels;

public class MonRevenueViewModel
{
    /// <summary>
    /// 公司代號
    /// </summary>
    public string CompanyId { get; set; }
    /// <summary>
    /// 出表日期
    /// </summary>
    public DateTime ReportDate { get; set; }
    /// <summary>
    /// 公司名稱
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 產業別
    /// </summary>
    public string Industry { get; set; }
    /// <summary>
    /// 資料年月
    /// </summary>
    public string DataYearMonth { get; set; }
    /// <summary>
    /// 營業收入-當月營收
    /// </summary>
    public decimal? RevenueCurrentMonth { get; set; }
    /// <summary>
    /// 營業收入-上月營收
    /// </summary>
    public decimal? RevenuePreviousMonth { get; set; }
    /// <summary>
    /// 營業收入-去年當月營收
    /// </summary>
    public decimal? RevenueSameMonthLastYear { get; set; }
    /// <summary>
    /// 營業收入-上月比較增減(%)
    /// </summary>
    public decimal? RevenueChangePreviousMonth { get; set; }
    /// <summary>
    /// 營業收入-去年同月增減(%)
    /// </summary>
    public decimal? RevenueChangeSameMonthLastYear { get; set; }
    /// <summary>
    /// 累計營業收入-當月累計營收
    /// </summary>
    public decimal? CumulativeRevenueCurrentMonth { get; set; }
    /// <summary>
    /// 累計營業收入-去年累計營收
    /// </summary>
    public decimal? CumulativeRevenueLastYear { get; set; }
    /// <summary>
    /// 累計營業收入-前期比較增減(%)
    /// </summary>
    public decimal? CumulativeRevenueChangePreviousPeriod { get; set; }
    /// <summary>
    /// 備註
    /// </summary>
    public string Notes { get; set; }
}
