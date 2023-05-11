using System.Collections;

namespace CoordinateRectanglesMatcher.Models;

public record SearchResult(Point CurrentPoint, IEnumerable<RectangleEntity> Rectangles );