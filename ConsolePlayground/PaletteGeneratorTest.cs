using GraphicsHelper;
using Microsoft.Extensions.DependencyInjection;
using PaletteGenerator;
using SixLabors.ImageSharp.PixelFormats;

namespace ConsolePlayground;

public static class PaletteGeneratorTest
{
    public static void Run(ServiceProvider provider)
    {
        IPaletteGenerator pg = provider.GetService<IPaletteGenerator>() ?? throw new InvalidOperationException();
        IImageConverter ic = provider.GetService<IImageConverter>() ?? throw new InvalidOperationException();

        var n = 5;
        var paletteMatrix = new Rgba32[50, 50 * n];

        var palette = pg.GeneratePalette(n, ColorBlindnessType.Monochromacy).Select(Rgba32.ParseHex).ToArray();

        for (int colorIndex = 0; colorIndex < n; colorIndex++)
        {
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    int matrixX = colorIndex * 50 + x;
                    paletteMatrix[y, matrixX] = palette[colorIndex];
                }
            }
        }

        ic.MatrixToImage(paletteMatrix, "/home/lesch4000/Загрузки/ImagePalette.png");
    }
}