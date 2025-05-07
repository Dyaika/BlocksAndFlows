namespace ColorSpaces.Spaces
{
    /// <summary>
    /// Computer matrix-like color space
    /// </summary>
    public class RGBColor
    {
        /// <summary>
        /// Red channel
        /// </summary>
        public double R { get; set; }

        /// <summary>
        /// Green channel
        /// </summary>
        public double G { get; set; }

        /// <summary>
        /// Blue channel
        /// </summary>
        public double B { get; set; }

        public RGBColor(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}