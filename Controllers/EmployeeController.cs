using API_Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Example.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EmployeeController : ControllerBase
{
    private readonly DatabaseContext databaseContext = new();

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        return await databaseContext.Employees.ToListAsync(); ;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        Employee employee = await databaseContext.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> NewEmployee(Employee employee)
    {
        if (employee == null) return BadRequest();
        databaseContext.Employees.Add(new Employee { Name = employee.Name, Salary = employee.Salary });
        await databaseContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<Employee>> DeleteEmployee(int id)
    {
        Employee employee = await databaseContext.Employees.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        databaseContext.Employees.Remove(employee);
        databaseContext.SaveChanges();
        return Ok();

    }
}
