using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GraphicsHelper;

public class ImageConverter : IImageConverter
{
    // Метод для преобразования изображения в матрицу цветов
    public Rgba32[,] ImageToMatrix(string imagePath)
    {
        // Загружаем изображение
        using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
        {
            int width = image.Width;
            int height = image.Height;

            // Создаем матрицу цветов с размерами изображения
            Rgba32[,] colorMatrix = new Rgba32[width, height];

            // Заполняем матрицу цветами пикселей изображения
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    colorMatrix[x, y] = image[x, y];
                }
            }

            return colorMatrix;
        }
    }

    // Метод для сохранения матрицы цветов обратно в изображение
    public void MatrixToImage(Rgba32[,] colorMatrix, string savePath)
    {
        int width = colorMatrix.GetLength(0);
        int height = colorMatrix.GetLength(1);

        // Создаем новое изображение
        using (Image<Rgba32> image = new Image<Rgba32>(width, height))
        {
            // Заполняем изображение пикселями из матрицы
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    image[x, y] = colorMatrix[x, y];
                }
            }

            // Сохраняем изображение в файл
            image.Save(savePath);
        }
    }
}