using SixLabors.ImageSharp.PixelFormats;

namespace GraphicsHelper;

/// <summary>
/// Converts images
/// </summary>
public interface IImageConverter
{
    /// <summary>
    /// Converts given image to matrix of colors
    /// </summary>
    /// <param name="imagePath">Path to image file</param>
    /// <returns>Matrix of colors</returns>
    public Rgba32[,] ImageToMatrix(string imagePath);

    /// <summary>
    /// Converts matrix of colors to 
    /// </summary>
    /// <param name="colorMatrix">Matrix of colors</param>
    /// <param name="savePath">Path to image file</param>
    void MatrixToImage(Rgba32[,] colorMatrix, string savePath);
}