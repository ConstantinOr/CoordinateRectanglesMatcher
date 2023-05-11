using CoordinateRectanglesMatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRectanglesMatcher.DAO;

public class CoordinateMatcherDbContext: DbContext
{
    public DbSet<RectangleEntity> Rectangle { get; set; }
    public CoordinateMatcherDbContext(DbContextOptions<CoordinateMatcherDbContext> options)
        : base(options)
    {
    }
}