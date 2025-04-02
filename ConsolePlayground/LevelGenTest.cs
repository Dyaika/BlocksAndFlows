using LevelGenerator;

namespace ConsolePlayground;

public static class LevelGenTest
{
    public static void Run()
    {
        ILevelGenerator g = new LevelGenerator.LevelGenerator();
        g.GenerateLevel();
    }
}