using LevelCore.Models;

namespace LevelGenerator;

public class Canvas
{
    public Canvas(Level level)
    {
        Level = level;
    }

    public Level Level { get; private set; }
    public Filter[,] GameField { get; private set; }
    public Filter[,] Storage { get; private set; }

    public void Update()
    {
        
    }
}