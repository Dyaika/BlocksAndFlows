using System;
using ColorSpaces.Spaces;

namespace ColorSpaces.Converters
{
    public class ColorMineColorConverter : IColorConverter
    {
        #region RGB

        private static double GammaToLinear(double srgb)
        {
            return (srgb <= 0.04045) ? srgb / 12.92 : Math.Pow((srgb + 0.055) / 1.055, 2.4);
        }

        private static double GammaToSRGB(double linear)
        {
            return (linear <= 0.0031308) ? linear * 12.92 : (1.055 * Math.Pow(linear, 1 / 2.4) - 0.055);
        }

        private double Denormalize(double normalized, double minValue = 0, double maxValue = 255.0)
        {
            var denormalized = minValue + normalized * (maxValue - minValue);
            if (denormalized < minValue)
            {
                return minValue;
            }

            if (denormalized > maxValue)
            {
                return maxValue;
            }

            return denormalized;
        }

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
                Denormalize(GammaToSRGB(color.R)),
                Denormalize(GammaToSRGB(color.G)),
                Denormalize(GammaToSRGB(color.B)));
        }

        public RGBColor SRGBToLinearRGB(RGBColor color)
        {
            return new RGBColor(
                GammaToLinear(color.R / 255.0f),
                GammaToLinear(color.G / 255.0f),
                GammaToLinear(color.B / 255.0f));
        }

        #endregion

        #region RGB - XYZ

        // sRGB → XYZ
        private static readonly double[,] RGB_TO_XYZ = new double[,]
        {
            { 0.4124, 0.3576, 0.1805 },
            { 0.2126, 0.7152, 0.0722 },
            { 0.0193, 0.1192, 0.9505 }
        };

        // XYZ → sRGB
        private static readonly double[,] XYZ_TO_RGB = new double[,]
        {
            { 3.2406255, -1.5372080, -0.4986286 },
            { -0.9689307, 1.8757561, 0.0415175 },
            { 0.0557101, -0.2040211, 1.0569959 }
        };

        public XYZColor SRGBToXYZ(RGBColor color)
        {
            color = SRGBToLinearRGB(color);
            double x = RGB_TO_XYZ[0, 0] * color.R + RGB_TO_XYZ[0, 1] * color.G + RGB_TO_XYZ[0, 2] * color.B;
            double y = RGB_TO_XYZ[1, 0] * color.R + RGB_TO_XYZ[1, 1] * color.G + RGB_TO_XYZ[1, 2] * color.B;
            double z = RGB_TO_XYZ[2, 0] * color.R + RGB_TO_XYZ[2, 1] * color.G + RGB_TO_XYZ[2, 2] * color.B;

            return new XYZColor(x, y, z);
        }

        public RGBColor XYZToSRGB(XYZColor color)
        {
            double r = XYZ_TO_RGB[0, 0] * color.X / 100.0 + XYZ_TO_RGB[0, 1] * color.Y / 100.0 + XYZ_TO_RGB[0, 2] * color.Z / 100.0;
            double g = XYZ_TO_RGB[1, 0] * color.X / 100.0 + XYZ_TO_RGB[1, 1] * color.Y / 100.0 + XYZ_TO_RGB[1, 2] * color.Z / 100.0;
            double b = XYZ_TO_RGB[2, 0] * color.X / 100.0 + XYZ_TO_RGB[2, 1] * color.Y / 100.0 + XYZ_TO_RGB[2, 2] * color.Z / 100.0;
            return LinearRGBToSRGB(new RGBColor(r, g, b));
        }

        #endregion

        #region RGB - CIE Lab

        public RGBColor CIELabToSRGB(CIELabColor color)
        {
            var xyz = CIELabToXYZ(color);
            return XYZToSRGB(xyz);
        }

        public CIELabColor SRGBToCIELab(RGBColor color)
        {
            var xyz = SRGBToXYZ(color);
            return XYZToCIELab(xyz);
        }

        #endregion

        #region LMS - XYZ

        private static readonly double[,] LMS_TO_XYZ = new double[,]
        {
            { 1.8601, -1.1295, 0.2199 },
            { 0.3612, 0.6388, 0.0000 },
            { 0.0000, 0.0000, 1.0891 }
        };

        private static readonly double[,] XYZ_TO_LMS = new double[,]
        {
            { 0.4002, 0.7075, -0.0808 },
            { -0.2263, 1.1653, 0.0457 },
            { 0.0000, 0.0000, 0.9182 }
        };

        // D65
        private const double Xn = 95.0489;
        private const double Yn = 100.000;
        private const double Zn = 108.884;

        public LMSColor XYZToLMS(XYZColor color)
        {
            double L = XYZ_TO_LMS[0, 0] * color.X + XYZ_TO_LMS[0, 1] * color.Y + XYZ_TO_LMS[0, 2] * color.Z;
            double M = XYZ_TO_LMS[1, 0] * color.X + XYZ_TO_LMS[1, 1] * color.Y + XYZ_TO_LMS[1, 2] * color.Z;
            double S = XYZ_TO_LMS[2, 0] * color.X + XYZ_TO_LMS[2, 1] * color.Y + XYZ_TO_LMS[2, 2] * color.Z;

            return new LMSColor(L, M, S);
        }

        public XYZColor LMStoXYZ(LMSColor color)
        {
            double x = LMS_TO_XYZ[0, 0] * color.L + LMS_TO_XYZ[0, 1] * color.M + LMS_TO_XYZ[0, 2] * color.S;
            double y = LMS_TO_XYZ[1, 0] * color.L + LMS_TO_XYZ[1, 1] * color.M + LMS_TO_XYZ[1, 2] * color.S;
            double z = LMS_TO_XYZ[2, 0] * color.L + LMS_TO_XYZ[2, 1] * color.M + LMS_TO_XYZ[2, 2] * color.S;
            return new XYZColor(x, y, z);
        }

        #endregion

        #region CIE Lab - XYZ

        private double F(double t)
        {
            const double delta = 6.0 / 29.0;
            if (t > (delta * delta * delta))
            {
                return Math.Pow(t, 1.0 / 3.0);
            }

            return (t / (3 * delta * delta)) + (4.0 / 29.0);
        }

        private double InverseF(double t)
        {
            const double delta = 6.0 / 29.0;
            if (t > delta)
            {
                return t * t * t;
            }

            return 3 * delta * delta * (t - 4.0 / 29.0);
        }

        public XYZColor CIELabToXYZ(CIELabColor color)
        {
            double x = Xn * InverseF((color.L + 16.0) / 116.0 + color.A / 500.0);
            double y = Yn * InverseF((color.L + 16.0) / 116.0);
            double z = Zn * InverseF((color.L + 16.0) / 116.0 - color.B / 200.0);
            return new XYZColor(x, y, z);
        }

        public CIELabColor XYZToCIELab(XYZColor color)
        {
            double l = 116.0 * F(color.Y / Yn) - 16;
            double a = 500.0 * (F(color.X / Xn) - F(color.Y / Yn));
            double b = 200 * (F(color.Y / Yn) - F(color.Z / Zn));
            return new CIELabColor(l, a, b);
        }

        #endregion

        #region CIE Lab - LMS

        public LMSColor CIELabToLMS(CIELabColor color)
        {
            var xyz = CIELabToXYZ(color);
            return XYZToLMS(xyz);
        }

        public CIELabColor LMSToCIELab(LMSColor color)
        {
            var xyz = LMStoXYZ(color);
            return XYZToCIELab(xyz);
        }

        #endregion
    }
}