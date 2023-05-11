using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CoordinateRectanglesMatcher.DAO;

public class CoordinateMatcherDbContextFactory: IDesignTimeDbContextFactory<CoordinateMatcherDbContext>
{
    public CoordinateMatcherDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<CoordinateMatcherDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CoordinateMatcherDbContext(optionsBuilder.Options);
    }
}