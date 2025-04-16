using LevelCore.Infrastructure;
using LevelCore.Models;

namespace LevelGenerator;

public class LevelGenerator : ILevelGenerator
{
    private const int FILTERS_HOLES_RATIO = 2;

    public Level GenerateLevel(int width, int height, byte colors, byte shapes, bool disassemble)
    {
        var matrix = new Filter?[width, height];

        // Mark filters to make holes
        MakeHoles(colors, shapes, matrix);

        // Bottom row, both color and shape are completely random
        GenerateBottomRow(colors, shapes, matrix);

        // Middle rows
        GenerateMiddleRows(colors, shapes, matrix);

        // Top row
        GenerateTopRow(colors, shapes, matrix);

        PrintMatrix(matrix); // delete this

        // Make level
        var level = new Level(width, height, MatrixToBlocks(matrix));
        if (disassemble)
        {
            level.Disassemble();
        }

        return level;
    }

    private void GenerateMiddleRows(byte colors, byte shapes, Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        for (var row = 1; row < height - 1; row++)
        {
            for (var col = 0; col < width; col++)
            {
                if (matrix[col, row] != null)
                {
                    matrix[col, row] = GenerateMatchingFilter(matrix[col, height - 1], colors: colors, shapes: shapes);
                    // Top row is used as mask to remember previous filters through spaces, it will be changed later
                    matrix[col, height - 1] = new Filter(matrix[col, row]);
                }
            }
        }
    }

    private void GenerateTopRow(byte colors, byte shapes, Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        for (var col = 0; col < width; col++)
        {
            matrix[col, height - 1] = GenerateMatchingFilter(matrix[col, height - 1], colors, shapes);
        }
    }

    private void GenerateBottomRow(byte colors, byte shapes, Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        var rand = new Random();
        for (var i = 0; i < width; i++)
        {
            var buffer = new byte[2];
            rand.NextBytes(buffer);
            matrix[i, 0] = new Filter(colorId: (byte)(buffer[0] % colors), shapeId: (byte)(buffer[1] % shapes));

            // Top row is used as mask to remember previous filters through spaces, it will be changed later
            matrix[i, height - 1] = new Filter(matrix[i, 0]);
        }
    }

    private void MakeHoles(byte colors, byte shapes, Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        // Initialise all filters with temporary values
        for (var row = 0; row < height; row++)
        {
            for (var col = 0; col < width; col++)
            {
                matrix[col, row] = new Filter(0, 0);
            }
        }

        var rand = new Random();
        for (var i = 0; i < width * height / FILTERS_HOLES_RATIO; i++)
        {
            var pos = rand.Next() % (width * (height - 2)) + width;
            matrix[pos % width, pos / width] = null;
        }
    }

    private Filter? GenerateMatchingFilter(Filter? prev, byte colors, byte shapes)
    {
        var rand = new Random();
        var buffer = new byte[2];
        rand.NextBytes(buffer);
        if (buffer[0] % 2 == 0)
        {
            // Save shape, change color
            return new Filter(colorId: (byte)(buffer[1] % colors), shapeId: prev.ShapeId);
        }

        // Save color, change shape
        return new Filter(colorId: prev.ColorId, shapeId: (byte)(buffer[1] % shapes));
    }

    private Block[] MatrixToBlocks(Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        var blocks = new List<Block>();
        var filters = new List<Filter>();
        var offset = 0;
        // Bottom
        for (var col = 0; col < width; col++)
        {
            filters.Add(new Filter(matrix[col, 0]));
        }

        blocks.Add(new Block(filters.ToArray(), offset, BlockType.Receiver, true));

        // Middle
        for (var row = 1; row < height - 1; row++)
        {
            filters.Clear();
            offset = width * row;
            for (var col = 0; col < width; col++)
            {
                if (matrix[col, row] != null)
                {
                    filters.Add(new Filter(matrix[col, row]));
                }
                else
                {
                    if (filters.Count > 0)
                    {
                        blocks.Add(new Block(filters.ToArray(), offset, BlockType.Converter, false));
                        filters.Clear();
                    }
                    offset = width * row + col + 1;
                }
            }

            if (filters.Count > 0)
            {
                blocks.Add(new Block(filters.ToArray(), offset, BlockType.Converter, false));
                filters.Clear();
            }
        }

        // Top
        offset = width * (height - 1);
        for (var col = 0; col < width; col++)
        {
            filters.Add(new Filter(matrix[col, height - 1]));
        }

        blocks.Add(new Block(filters.ToArray(), offset, BlockType.Producer, true));
        return blocks.ToArray();
    }

    private void PrintMatrix(Filter?[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        for (var row = height - 1; row >= 0; row--)
        {
            for (var col = 0; col < width; col++)
            {
                if (matrix[col, row] != null)
                {
                    Console.Write($"{matrix[col, row].ColorId}{matrix[col, row].ShapeId}\t");
                }
                else
                {
                    Console.Write($"**\t");
                }
            }

            Console.WriteLine();
        }
    }
}