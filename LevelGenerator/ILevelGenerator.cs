namespace LevelGenerator;

public interface ILevelGenerator
{
    public const byte DEFAULT_COLORS_COUNT = 5;
    public const byte DEFAULT_SHAPES_COUNT = 5;
    public const int DEFAULT_WIDTH = 7;
    public const int DEFAULT_HEIGHT = 7;
    
    Level GenerateLevel(
        int width = DEFAULT_WIDTH,
        int height = DEFAULT_HEIGHT,
        byte colors = DEFAULT_COLORS_COUNT,
        byte shapes = DEFAULT_SHAPES_COUNT);
}