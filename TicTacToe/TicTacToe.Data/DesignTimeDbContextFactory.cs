using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TicTacToe.Data;

namespace StoronnimV.Data;

/// <summary>
/// Класс, который используется для создания объекта контекста для разработки.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TicTacToeContext>
{
    public TicTacToeContext CreateDbContext(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var relativePath = "../TicTacToe.Api";
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(currentDirectory, relativePath))
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("PostgreSQL");

        var optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TicTacToeContext(optionsBuilder.Options);
    }
}