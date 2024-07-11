using AutoMapper;
using ListedCompany.Models;
using ListedCompany.ViewModels;
using ListedCompany.Services.Repository.UnitOfWork;
using ListedCompany.Services.IService;
using ListedCompany.CustomSQL;

namespace ListedCompany.Services;

public class MonthlyRevenueService : GenericService<MonRevenue>, IMonthlyRevenueService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MonthlyRevenueService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// 使用篩選條件查詢資料
    /// </summary>
    public async Task<IEnumerable<MonRevenueViewModel>> QueryMonthlyRevenuesFilterAsync(string companyId)
    {
        var queryMonthlyRevenue = new MonthlyRevenueSQL();
        var sqlCommand = queryMonthlyRevenue.GetFilterCommand();

        var parameters = new
        {
            companyId,
        };
        var results = await _unitOfWork.Repository<MonRevenue>()
            .ExecuteQuery<MonRevenueViewModel>(sqlCommand, parameters);

        return results;
    }

    /// <summary>
    /// 使用預存程序查詢資料
    /// </summary>
    public async Task<IEnumerable<MonRevenueViewModel>> QueryMonthlyRevenuesAsync()
    {
        var results = await _unitOfWork.Repository<MonRevenue>()
            .ExecuteSPQuery<MonRevenueViewModel>("spQueryMonthlyRevenues");
        return results;
    }

    /// <summary>
    /// 使用預存程序新增資料
    /// </summary>
    public async Task<bool> AddMonthlyRevenueAsync(MonRevenueViewModel revenueViewModel)
    {
        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                var parameters = new
                {
                    revenueViewModel.CompanyId,
                    revenueViewModel.CompanyName,
                    revenueViewModel.Industry,
                    revenueViewModel.ReportDate,
                    revenueViewModel.DataYearMonth,
                    revenueViewModel.RevenueCurrentMonth,
                    revenueViewModel.RevenuePreviousMonth,
                    revenueViewModel.RevenueSameMonthLastYear,
                    revenueViewModel.RevenueChangePreviousMonth,
                    revenueViewModel.RevenueChangeSameMonthLastYear,
                    revenueViewModel.CumulativeRevenueCurrentMonth,
                    revenueViewModel.CumulativeRevenueLastYear,
                    revenueViewModel.CumulativeRevenueChangePreviousPeriod,
                    revenueViewModel.Notes
                };

                await _unitOfWork.Repository<MonRevenue>().ExecuteSP("spAddMonRevenueAndCompanyData", parameters);

                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
