using LevelCore.Infrastructure;
using LevelCore.Models;
using LevelGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace ConsolePlayground;

public static class LevelGenTest
{
    public static void Run(ServiceProvider provider)
    {
        var height = 5;
        var id = 10;
        ILevelGenerator g = provider.GetService<ILevelGenerator>();
        Console.WriteLine("-----Generating level-----");
        var level = g.GenerateLevel(7, height);
        var content = LevelSerializer.Serialize(level);
        var lv = LevelSerializer.Deserialize(content);
        File.WriteAllText($"/mnt/HardDrive/downloads/Levels/level{id}.json", content);
        Console.WriteLine("-----Level generated-----");
        PrintMatrix(lv.SimulateAsMatrix());
        Console.WriteLine("-----Score-----");
        Console.WriteLine("-----Level generated-----");
        PrintMatrix(level.SimulateAsMatrix());
        Console.WriteLine("-----Score-----");
        Console.WriteLine(level.CalculateResult().Score);
        level.Disassemble();
        Console.WriteLine("-----Level disassembled-----");
        PrintMatrix(level.SimulateAsMatrix());
        Console.WriteLine("-----Score-----");
        Console.WriteLine(level.CalculateResult().Score);
    }

    private static void PrintMatrix(Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        for (var row = height - 1; row >= 0; row--)
        {
            for (var col = 0; col < width; col++)
            {
                if (matrix[col, row] != null)
                {
                    if (matrix[col, row].IsBroken)
                    {
                        
                        Console.Write($"XX\t");
                    }
                    else
                    {
                        Console.Write($"{matrix[col, row].ColorId}{matrix[col, row].ShapeId}\t");
                    }
                }
                else
                {
                    Console.Write($"  \t");
                }
            }

            Console.WriteLine();
        }
    }
}