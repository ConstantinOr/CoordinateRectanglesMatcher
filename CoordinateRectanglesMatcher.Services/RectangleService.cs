
using System.Data.Entity;
using CoordinateRectanglesMatcher.DAO;
using CoordinateRectanglesMatcher.Models;

namespace CoordinateRectanglesMatcher.Services;

public class RectangleService: IRectangleService
{
    private const int RectangleCount = 200;
    private readonly CoordinateMatcherDbContext _dbContext;
    public RectangleService(CoordinateMatcherDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<RectangleEntity> GenerateUniqueRectangles(int numberOfRectangles)
    {
        var uniqueRectangles = new List<RectangleEntity>();
        var random = new Random();

        for (var i = 0; i < numberOfRectangles; i++)
        {
            RectangleEntity rectangleEntity;
            do
            {
                var x = random.NextInt64() * 100;
                var y = random.NextInt64() * 100;
                var width = random.NextInt64() * 50;
                var height = random.NextInt64() * 50;

                rectangleEntity = new RectangleEntity
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                };
            } while (uniqueRectangles.Exists(r => AreRectanglesEqual(r, rectangleEntity)));

            uniqueRectangles.Add(rectangleEntity);
        }

        return uniqueRectangles;
    }

    public async Task<bool> Seed()
    {
        await _dbContext.Rectangle.AddRangeAsync(GenerateUniqueRectangles(RectangleCount));
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public IEnumerable<SearchResult> Search(IEnumerable<Point> points)
    {
        var rectagles =  _dbContext.Rectangle.OrderBy(x => x.Id).ToList();

        var enumerable = points as Point[] ?? points.ToArray();
        if (!enumerable.Any())
        {
            return new List<SearchResult>();
        }

        return enumerable.Select(p =>
        {
            return new SearchResult(p, rectagles.Where(x => x.ContainsPoint(p)).ToList());
        }).ToList();
    }


    private bool AreRectanglesEqual(RectangleEntity r1, RectangleEntity r2)
    {
        return r1.X == r2.X && r1.Y == r2.Y && r1.Width == r2.Width && r1.Height == r2.Height;
    }
}

