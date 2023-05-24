using CoordinateRectanglesMatcher.Models.DTO;

namespace CoordinateRectanglesMatcher.Models;

public class RectangleEntity: IEntity 
{
    public long Id { get; set; } // Id - auto-incremented pseudo primary key
    public long X { get; set; } // X-coordinate of the bottom-left corner
    public long Y { get; set; } // Y-coordinate of the bottom-left corner
    public long Width { get; set; } // Width of the rectangle
    public long Height { get; set; } // Height of the rectangle
}