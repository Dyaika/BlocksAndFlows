namespace LevelGenerator;

public class LevelGenerator: ILevelGenerator
{
    private const int FILTERS_HOLES_RATIO = 2;
    public Level GenerateLevel(
        int width = ILevelGenerator.DEFAULT_WIDTH,
        int height = ILevelGenerator.DEFAULT_HEIGHT,
        byte colors = ILevelGenerator.DEFAULT_COLORS_COUNT,
        byte shapes = ILevelGenerator.DEFAULT_SHAPES_COUNT)
    {
        var matrix = new Filter?[width, height];

        // Mark filters to make holes
        MakeHoles(width, height, colors, shapes, matrix);

        PrintMatrix(matrix); // delete this
        
        // Bottom row, both color and shape are completely random
        GenerateBottomRow(width, height, colors, shapes, matrix);

        // Middle rows
        GenerateMiddleRows(width, height);

        // Top row
        GenerateTopRow(width, height, colors, shapes, matrix);

        throw new NotImplementedException();
    }

    private void GenerateMiddleRows(int width, int height)
    {
        for (var row = 1; row < height - 1; row++)
        {
            for (var col = 0; col < width; col++)
            {
                // TODO: fill mid layers
            }
        }
    }

    private void GenerateTopRow(int width, int height, byte colors, byte shapes, Filter?[,] matrix)
    {
        for (var col = 0; col < width; col++)
        {
            matrix[col, height - 1] = GenerateMatchingFilter(matrix[col, height - 1], colors, shapes);
        }
    }

    private void GenerateBottomRow(int width, int height, byte colors, byte shapes, Filter?[,] matrix)
    {
        int i;
        var rand = new Random();
        for (i = 0; i < width; i++)
        {
            var buffer = new byte[2];
            rand.NextBytes(buffer);
            matrix[i, 0] = new Filter(colorId: (byte)(buffer[0] / colors), shapeId: (byte)(buffer[1] / shapes));

            // Top row is used as mask to remember previous filters through spaces, it will be changed later
            matrix[i, height - 1] = new Filter(matrix[i, 0]);
        }
    }

    private void MakeHoles(int width, int height, byte colors, byte shapes, Filter?[,] matrix)
    {
        // initialise all filters with temporary values
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
            matrix[pos / width, pos % width] = null;
        }
    }

    private Filter? GenerateMatchingFilter(Filter? prev, byte colors, byte shapes)
    {
        var rand = new Random();
        var buffer = new byte[2];
        if (buffer[0] % 2 == 0)
        {
            // save shape, change color
            return new Filter(colorId: (byte)(buffer[1] % colors), shapeId: prev.ShapeId);
        }

        // save color, change shape
        return new Filter(colorId: prev.ColorId, shapeId: (byte)(buffer[1] % shapes));
    }

    private void PrintMatrix(Filter?[,] matrix)
    {
        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            for (var col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col] != null)
                {
                    Console.Write($"{matrix[row, col].ColorId}{matrix[row, col].ShapeId}\t");
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