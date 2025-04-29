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

        var n = 5; // Количество цветов в одной палитре
        var types = new[]
        {
            ColorBlindnessType.None,
            ColorBlindnessType.Protanopy,
            ColorBlindnessType.Deuteranopy,
            ColorBlindnessType.Tritanopy,
            ColorBlindnessType.Monochromacy
        };

        int paletteHeight = 50;
        int paletteWidth = 50 * n;

        var paletteMatrix = new Rgba32[paletteHeight * types.Length, paletteWidth];

        for (int typeIndex = 0; typeIndex < types.Length; typeIndex++)
        {
            var palette = pg.GeneratePalette(n, types[typeIndex])
                .Select(Rgba32.ParseHex)
                .ToArray();

            for (int colorIndex = 0; colorIndex < n; colorIndex++)
            {
                for (int y = 0; y < paletteHeight; y++)
                {
                    for (int x = 0; x < 50; x++)
                    {
                        int matrixX = colorIndex * 50 + x;
                        int matrixY = typeIndex * paletteHeight + y;
                        paletteMatrix[matrixY, matrixX] = palette[colorIndex];
                    }
                }
            }
        }

        ic.MatrixToImage(paletteMatrix, "/home/lesch4000/Загрузки/ImagePalette.png");
    }
}