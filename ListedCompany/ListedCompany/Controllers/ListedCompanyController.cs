﻿using Microsoft.AspNetCore.Mvc;


namespace ListedCompany.Controllers;

[Route("[controller]")]
[ApiController]
public class ListedCompanyController : ControllerBase
{
    
    [HttpGet]
    public IEnumerable<string> Get()
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

    
    [HttpPatch("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
