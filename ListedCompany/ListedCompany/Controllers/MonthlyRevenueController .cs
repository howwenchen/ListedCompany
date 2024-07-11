using ListedCompany.Services.IService;
using ListedCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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

    
    [HttpGet("Filter")]
    public async Task<ActionResult<IEnumerable<MonRevenueViewModel>>> GetFilter(string companyId)
    {
        var result = await _monthlyRevenueService.QueryMonthlyRevenuesFilterAsync(companyId);

        return result.ToList();
    }

    
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    
    [HttpPatch]
    public void Patch(int id, [FromBody] string value)
    {
    }

    
    [HttpDelete]
    public void Delete(int id)
    {
    }
}
