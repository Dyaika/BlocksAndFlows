namespace ColorSpaces.Spaces
{
    /// <summary>
    /// Color space with human-like distances between colors
    /// </summary>
    public class CIELabColor
    {
        /// <summary>
        /// Minimal L value
        /// </summary>
        public static double MinL => 0.0;

        /// <summary>
        /// Maximal L value
        /// </summary>
        public static double MaxL => 100.0;

        /// <summary>
        /// Minimal a value
        /// </summary>
        public static double MinA => -128.0;

        /// <summary>
        /// Maximal a value
        /// </summary>
        public static double MaxA => 128.0;

        /// <summary>
        /// Minimal b value
        /// </summary>
        public static double MinB => -128.0;

        /// <summary>
        /// Maximal b value
        /// </summary>
        public static double MaxB => 128.0;

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