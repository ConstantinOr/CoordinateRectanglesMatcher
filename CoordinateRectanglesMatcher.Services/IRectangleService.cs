
using CoordinateRectanglesMatcher.DAO;
using CoordinateRectanglesMatcher.Models;

namespace CoordinateRectanglesMatcher.Services;

public interface IRectangleService
{
    IEnumerable<RectangleEntity> GenerateUniqueRectangles(int numberOfRectangles);
    Task<bool> Seed();
    IEnumerable<SearchResult> Search(IEnumerable<Point> points);
}