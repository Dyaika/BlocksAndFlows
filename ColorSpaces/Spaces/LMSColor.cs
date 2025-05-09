namespace ColorSpaces.Spaces
{
    /// <summary>
    /// Physically correct color space
    /// </summary>
    public class LMSColor
    {
        /// <summary>
        /// Long-wavelength (red)
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// Medium-wavelength (green)
        /// </summary>
        public double M { get; set; }

        /// <summary>
        /// Short-wavelength (blue)
        /// </summary>
        public double S { get; set; }

        public LMSColor(double l, double m, double s)
        {
            L = l;
            M = m;
            S = s;
        }
    }
}