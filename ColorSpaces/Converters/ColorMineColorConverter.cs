using System;
using ColorMine.ColorSpaces;
using ColorSpaces.Spaces;

namespace ColorSpaces.Converters
{
    public class ColorMineColorConverter : IColorConverter
    {
        public string sRGBToHex(RGBColor color)
        {
            int r = (int)(color.R);
            int g = (int)(color.G);
            int b = (int)(color.B);

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public RGBColor LinearRGBToSRGB(RGBColor color)
        {
            return new RGBColor(
                ToSRGB(color.R / 255.0f),
                ToSRGB(color.G / 255.0f),
                ToSRGB(color.B / 255.0f));
        }

        public RGBColor SRGBToLinearRGB(RGBColor color)
        {
            return new RGBColor(
                ToLinear(color.R / 255.0f),
                ToLinear(color.G / 255.0f),
                ToLinear(color.B / 255.0f));
        }

        public RGBColor CIELabToSRGB(CIELabColor color)
        {
            var lab = new Lab() { L = color.L, A = color.A, B = color.B };
            var rgb = lab.To<Rgb>();
            return new RGBColor(rgb.R, rgb.G, rgb.B);
        }

        public CIELabColor SRGBToCIELab(RGBColor color)
        {
            var rgb = new Rgb() { R = color.R, G = color.G, B = color.B };
            var lab = rgb.To<Lab>();
            return new CIELabColor(lab.L, lab.A, lab.B);
        }

        public LMSColor CIELabToLMS(CIELabColor color)
        {
            var lab = new Lab() { L = color.L, A = color.A, B = color.B };
            var xyz = lab.To<Xyz>();

            double l = 0.4002 * xyz.X + 0.7076 * xyz.Y - 0.0808 * xyz.Z;
            double m = -0.2263 * xyz.X + 1.1653 * xyz.Y + 0.0457 * xyz.Z;
            double s = 0.0000 * xyz.X + 0.0000 * xyz.Y + 0.9182 * xyz.Z;

            return new LMSColor(l, m, s);
        }

        public CIELabColor LMSToCIELab(LMSColor color)
        {
            double x = 1.8599 * color.L - 1.1294 * color.M + 0.2199 * color.S;
            double y = 0.3612 * color.L + 0.6388 * color.M;
            double z = 1.0891 * color.S;

            var lab = new Xyz() { X = x, Y = y, Z = z }.To<Lab>();

            return new CIELabColor(lab.L, lab.A, lab.B);
        }

        private static double ToLinear(double srgb)
        {
            return (srgb <= 0.04045) ? srgb / 12.92 : Math.Pow((srgb + 0.055) / 1.055, 2.4);
        }

        private static double ToSRGB(double linear)
        {
            return (linear <= 0.0031308f) ? linear * 12.92f : (1.055 * Math.Pow(linear, 1 / 2.4) - 0.055);
        }
    }
}