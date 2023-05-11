namespace CoordinateRectanglesMatcher.Models;

public interface IEntity
{
    long Id { get; set; }

    bool ContainsPoint(Point point);
}