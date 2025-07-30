using API_Example.Models;
using Microsoft.OpenApi.Models;

namespace API_Example;

public class Program
{
    private readonly static DatabaseContext databaseContext = new();
    public static void Main(string[] args)
    {
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        databaseContext.Employees.Add(new Employee { Name = "Bill", Salary = 19200 });
        databaseContext.Employees.Add(new Employee { Name = "Jane", Salary = 17600 });
        databaseContext.Employees.Add(new Employee { Name = "John", Salary = 11900 });
        databaseContext.Employees.Add(new Employee { Name = "Aaron", Salary = 20400 });
        databaseContext.Employees.Add(new Employee { Name = "Catherine", Salary = 21500 });
        databaseContext.SaveChanges();

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API-Example",
                Description = "Simple API Example",
            });
        });
        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.DocumentTitle = "API-Example";
            o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            o.RoutePrefix = string.Empty;
        });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
