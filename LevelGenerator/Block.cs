namespace LevelGenerator;

public class Block
{
    public int Offset { get; set; }
    public BlockType Type { get; init; }
    private readonly Filter[] _filters;
    public Filter[] Filters
    {
        get => _filters.ToArray();
        init => _filters = value ?? [];
    }
}