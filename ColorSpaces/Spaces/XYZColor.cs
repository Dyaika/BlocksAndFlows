namespace ColorSpaces.Spaces
{
    /// <summary>
    /// CIE XYZ color space
    /// </summary>
    public class XYZColor
    {
        /// <summary>
        /// X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Z
        /// </summary>
        public double Z { get; set; }

        public XYZColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}