namespace PaletteGenerator
{
    /// <summary>
    /// Generator of color palettes
    /// </summary>
    public interface IPaletteGenerator
    {
        /// <summary>
        /// Generate palette
        /// </summary>
        /// <param name="n">Number of colors</param>
        /// <param name="type">Color blindness type</param>
        /// <returns>Collection of hex colors</returns>
        string[] GeneratePalette(int n, ColorBlindnessType type = ColorBlindnessType.None);
    }
}