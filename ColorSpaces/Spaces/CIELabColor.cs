namespace PaletteGenerator.ColorConverter.ColorSpaces
{
    /// <summary>
    /// Color space with human-like distances between colors
    /// </summary>
    public class CIELabColor
    {
        /// <summary>
        /// Lightness
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// Green–Red component
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Blue–Yellow component
        /// </summary>
        public double B { get; set; }

        public CIELabColor(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }
    }
}