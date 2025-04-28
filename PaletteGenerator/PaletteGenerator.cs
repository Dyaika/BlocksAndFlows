using System;
using PaletteGenerator.ColorConverter;
using PaletteGenerator.ColorConverter.ColorSpaces;

namespace PaletteGenerator
{
    /// <inheritdoc />
    public class PaletteGenerator : IPaletteGenerator
    {
        private IColorConverter _colorConverter;

        public PaletteGenerator(IColorConverter colorConverter)
        {
            _colorConverter = colorConverter ?? throw new ArgumentNullException(nameof(colorConverter));
        }

        /// <inheritdoc />
        public string[] GeneratePalette(int n, ColorBlindnessType type = ColorBlindnessType.None)
        {
            switch (type)
            {
                case ColorBlindnessType.Protanopy:
                    return GenerateProtanopyPalette(n);
                case ColorBlindnessType.Deuteranopy:
                    return GenerateDeuteranopyPalette(n);
                case ColorBlindnessType.Tritanopy:
                    return GenerateTritanopyPalette(n);
                case ColorBlindnessType.Monochromacy:
                    return GenerateMonochromacyPalette(n);
                case ColorBlindnessType.None:
                default:
                    return GenerateDefaultPalette(n);
            }
        }

        private string[] GenerateDefaultPalette(int n)
        {
            throw new NotImplementedException();
        }

        private string[] GenerateProtanopyPalette(int n)
        {
            throw new NotImplementedException();
        }

        private string[] GenerateDeuteranopyPalette(int n)
        {
            throw new NotImplementedException();
        }

        private string[] GenerateTritanopyPalette(int n)
        {
            throw new NotImplementedException();
        }

        private string[] GenerateMonochromacyPalette(int n)
        {
            if (n == 1)
            {
                return new[] { "#FFFFFF" };
            }

            var palette = new string[n];
            var lRange = CIELabColor.MaxL - CIELabColor.MinL;
            for (int i = 0; i < n; i++)
            {
                var color = new CIELabColor(lRange * i / (n - 1.0), 0, 0);
                palette[i] = _colorConverter.sRGBToHex(_colorConverter.CIELabToSRGB(color));
            }

            return palette;
        }
    }
}