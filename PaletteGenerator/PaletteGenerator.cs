using System;
using System.Collections.Generic;
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
            if (n == 1)
            {
                return new[] { "#FFFFFF" };
            }

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
            var palette = new List<CIELabColor>();
            var samples = new List<CIELabColor>();
            var random = new Random();

            int nSamples = 100;

            for (int i = 0; i < nSamples; i++)
            {
                double l = CIELabColor.MinL + random.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MinA + random.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA);
                double b = CIELabColor.MinB + random.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);

                var color = new CIELabColor(l, a, b);

                samples.Add(color);
            }

            if (samples.Count == 0)
                throw new Exception("No valid samples found!");

            palette.Add(samples[random.Next(samples.Count)]);

            while (palette.Count < n)
            {
                double maxMinDist = -1;
                CIELabColor bestCandidate = null;

                foreach (var sample in samples)
                {
                    double minDist = double.MaxValue;

                    foreach (var existing in palette)
                    {
                        double dist = LabDistance(existing, sample);
                        if (dist < minDist)
                            minDist = dist;
                    }

                    if (minDist > maxMinDist)
                    {
                        maxMinDist = minDist;
                        bestCandidate = sample;
                    }
                }

                palette.Add(bestCandidate);
            }

            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(palette[i]);
                hexPalette[i] = _colorConverter.sRGBToHex(rgb);
            }

            return hexPalette;
        }

        private string[] GenerateProtanopyPalette(int n)
        {
            var palette = new List<CIELabColor>();
            var samples = new List<CIELabColor>();
            var random = new Random();

            int nSamples = 100;

            for (int i = 0; i < nSamples; i++)
            {
                double l = CIELabColor.MinL + random.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MinA + random.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA) / 2;
                double b = CIELabColor.MinB + random.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);

                var color = new CIELabColor(l, a, b);

                samples.Add(color);
            }

            if (samples.Count == 0)
                throw new Exception("No valid samples found!");

            palette.Add(samples[random.Next(samples.Count)]);

            while (palette.Count < n)
            {
                double maxMinDist = -1;
                CIELabColor bestCandidate = null;

                foreach (var sample in samples)
                {
                    double minDist = double.MaxValue;

                    foreach (var existing in palette)
                    {
                        double dist = LabDistance(existing, sample);
                        if (dist < minDist)
                            minDist = dist;
                    }

                    if (minDist > maxMinDist)
                    {
                        maxMinDist = minDist;
                        bestCandidate = sample;
                    }
                }

                palette.Add(bestCandidate);
            }

            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(palette[i]);
                hexPalette[i] = _colorConverter.sRGBToHex(rgb);
            }

            return hexPalette;
        }

        private string[] GenerateDeuteranopyPalette(int n)
        {
            var palette = new List<CIELabColor>();
            var samples = new List<CIELabColor>();
            var random = new Random();

            int nSamples = 100;

            for (int i = 0; i < nSamples; i++)
            {
                double l = CIELabColor.MinL + random.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MaxA - random.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA) / 2;
                double b = CIELabColor.MinB + random.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);

                var color = new CIELabColor(l, a, b);

                samples.Add(color);
            }

            if (samples.Count == 0)
                throw new Exception("No valid samples found!");

            palette.Add(samples[random.Next(samples.Count)]);

            while (palette.Count < n)
            {
                double maxMinDist = -1;
                CIELabColor bestCandidate = null;

                foreach (var sample in samples)
                {
                    double minDist = double.MaxValue;

                    foreach (var existing in palette)
                    {
                        double dist = LabDistance(existing, sample);
                        if (dist < minDist)
                            minDist = dist;
                    }

                    if (minDist > maxMinDist)
                    {
                        maxMinDist = minDist;
                        bestCandidate = sample;
                    }
                }

                palette.Add(bestCandidate);
            }

            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(palette[i]);
                hexPalette[i] = _colorConverter.sRGBToHex(rgb);
            }

            return hexPalette;
        }

        private string[] GenerateTritanopyPalette(int n)
        {
            var palette = new List<CIELabColor>();
            var samples = new List<CIELabColor>();
            var random = new Random();

            int nSamples = 100;

            for (int i = 0; i < nSamples; i++)
            {
                double l = CIELabColor.MinL + random.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MinA + random.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA);
                double b = CIELabColor.MaxB - random.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);

                var color = new CIELabColor(l, a, b);

                samples.Add(color);
            }

            if (samples.Count == 0)
                throw new Exception("No valid samples found!");

            palette.Add(samples[random.Next(samples.Count)]);

            while (palette.Count < n)
            {
                double maxMinDist = -1;
                CIELabColor bestCandidate = null;

                foreach (var sample in samples)
                {
                    double minDist = double.MaxValue;

                    foreach (var existing in palette)
                    {
                        double dist = LabDistance(existing, sample);
                        if (dist < minDist)
                            minDist = dist;
                    }

                    if (minDist > maxMinDist)
                    {
                        maxMinDist = minDist;
                        bestCandidate = sample;
                    }
                }

                palette.Add(bestCandidate);
            }

            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(palette[i]);
                hexPalette[i] = _colorConverter.sRGBToHex(rgb);
            }

            return hexPalette;
        }

        private string[] GenerateMonochromacyPalette(int n)
        {
            var palette = new string[n];
            var lRange = CIELabColor.MaxL - CIELabColor.MinL;
            for (int i = 0; i < n; i++)
            {
                var color = new CIELabColor(CIELabColor.MinL + lRange * i / (n - 1.0), 0, 0);
                palette[i] = _colorConverter.sRGBToHex(_colorConverter.CIELabToSRGB(color));
            }

            return palette;
        }

        private double LabDistance(CIELabColor c1, CIELabColor c2)
        {
            return Math.Sqrt(
                Math.Pow(c1.L - c2.L, 2) +
                Math.Pow(c1.A - c2.A, 2) +
                Math.Pow(c1.B - c2.B, 2)
            );
        }
    }
}