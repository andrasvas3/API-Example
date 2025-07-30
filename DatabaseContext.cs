using API_Example.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Example;

class DatabaseContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING"));
    }
}
