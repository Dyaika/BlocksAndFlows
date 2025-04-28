using System;
using ColorMine.ColorSpaces;
using PaletteGenerator.ColorConverter.ColorSpaces;

namespace PaletteGenerator.ColorConverter
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
            var lab = new Lab(){ L = color.L, A = color.A, B = color.B };
            var rgb = lab.To<Rgb>();
            return new RGBColor(rgb.R, rgb.G, rgb.B);
        }

        public CIELabColor SRGBToCIELab(RGBColor color)
        {
            var rgb = new Rgb() { R = color.R, G = color.G, B = color.B };
            var lab = rgb.To<Lab>();
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