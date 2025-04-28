using System.Drawing;
using GraphicsHelper;
using PaletteGenerator.ColorConverter;
using PaletteGenerator.ColorConverter.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using ImageConverter = GraphicsHelper.ImageConverter;

namespace ConsolePlayground;

public static class GraphicsHelperTest
{
    public static void Run()
    {
        IImageConverter ic = new ImageConverter();
        IColorConverter cc = new ColorMineColorConverter();
        var img = ic.ImageToMatrix("/home/lesch4000/Загрузки/ImageInput.png");
        for (var i = 0; i < img.GetLength(0); i++)
        {
            for (var j = 0; j < img.GetLength(1); j++)
            {
                var srgb = new RGBColor(img[i, j].R, img[i, j].G, img[i, j].B);
                var lab = cc.SRGBToCIELab(srgb);
                var srgb2 = cc.CIELabToSRGB(lab);
                img[i, j] = Rgba32.ParseHex(cc.sRGBToHex(srgb2));
            }
        }
        ic.MatrixToImage(img, "/home/lesch4000/Загрузки/ImageOutput.png");
    }
}