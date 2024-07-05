using ListedCompany.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace ListedCompany.Controllers;


[Route("[controller]/[action]")]
[ApiController]
public class ListedCompanyController : ControllerBase
{
    
    [HttpGet]
    public IEnumerable<string> Get(MonRevenueViewModel viewModel)
    {
        return new string[] { "value1", "value2" };
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
