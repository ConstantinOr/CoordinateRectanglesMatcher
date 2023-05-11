using CoordinateRectanglesMatcher.Models.DTO;

namespace CoordinateRectanglesMatcher.Models;

public class RectangleEntity: RectangleDto, IEntity 
{
    public long Id { get; set; }
    public bool ContainsPoint(Point point)
    {
        return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
    }
}