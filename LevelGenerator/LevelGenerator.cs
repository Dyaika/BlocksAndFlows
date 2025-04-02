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
        Filter[,] matrix = new Filter[width, height];
        var rand = new Random();
        byte[] buffer;
        
        // Mark filters to make holes
        int i;
        for (i = 0; i < width * height / FILTERS_HOLES_RATIO; i++)
        {
            var pos = rand.Next() % (width * (height - 2)) + width;
            matrix[pos / width, pos % width] = new Filter(colorId: colors, shapeId: shapes);
        }

        PrintMatrix(matrix); // delete this
        
        // Bottom row, both color and shape are completely random
        for (i = 0; i < width; i++)
        {
            buffer = new byte[2];
            rand.NextBytes(buffer);
            matrix[i, 0] = new Filter(colorId: (byte)(buffer[0] / colors), shapeId: (byte)(buffer[1] / shapes));
            
            // Top row is used as mask to remember previous filters through spaces, it will be changed later
            matrix[i, height-1] = new Filter(matrix[i, 0]);
        }

        // Middle rows
        for (var row = 1; row < height - 1; row++)
        {
            for (var col = 0; col < width; col++)
            {
                // TODO: fill mid layers
            }
        }
        
        // Top row
        for (var col = 0; col < width; col++)
        {
            // TODO: fill top layer
        }

        throw new NotImplementedException();
    }

    private void PrintMatrix(Filter[,] matrix)
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