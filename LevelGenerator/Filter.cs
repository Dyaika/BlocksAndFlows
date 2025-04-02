namespace LevelGenerator;

public class Filter
{
    public byte ColorId { get; init; }
    public byte ShapeId { get; init; }

    public Filter(byte colorId, byte shapeId)
    {
        ColorId = colorId;
        ShapeId = shapeId;
    }

    public Filter(Filter other)
    {
        ColorId = other.ColorId;
        ShapeId = other.ShapeId;
    }
}