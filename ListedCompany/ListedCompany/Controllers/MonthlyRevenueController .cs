using ListedCompany.Services.IService;
using ListedCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ListedCompany.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class MonthlyRevenueController : ControllerBase
{
    private readonly IMonthlyRevenueService _monthlyRevenueService;

    public MonthlyRevenueController(IMonthlyRevenueService monthlyRevenueService)
    {
        _monthlyRevenueService = monthlyRevenueService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MonRevenueViewModel>>> Get()
    {
        var result = await _monthlyRevenueService.QueryMonthlyRevenuesAsync();

        return result.ToList();
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MonRevenueViewModel>>> GetFilter([FromQuery]string companyId)
    {
        var result = await _monthlyRevenueService.QueryMonthlyRevenuesFilterAsync(companyId);
        
        return result.ToList();
    }

    
    [HttpPost]
    public async Task<bool> Post([FromBody] MonRevenueViewModel revenueViewModel)
    {
        var result = await _monthlyRevenueService.AddMonthlyRevenueAsync(revenueViewModel);

        return result;
    }

}
