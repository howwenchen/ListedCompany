using ListedCompany.Services.Repository.UnitOfWork;
using ListedCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace ListedCompany.Controllers;


[Route("[controller]/[action]")]
[ApiController]
public class MonthlyRevenueController : ControllerBase
{

    public MonthlyRevenueController()
    {
    }

    [HttpGet]
    public IEnumerable<MonRevenueViewModel> Get()
    {
        var result = new List<MonRevenueViewModel>();
        return result;
    }

    
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
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
