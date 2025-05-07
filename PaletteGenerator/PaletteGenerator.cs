using System;
using System.Collections.Generic;
using System.Linq;
using ColorBlindnessSimulator;
using ColorSpaces.Converters;
using ColorSpaces.Spaces;

namespace PaletteGenerator
{
    /// <inheritdoc />
    public class PaletteGenerator : IPaletteGenerator
    {
        private IColorConverter _colorConverter;
        private IColorBlindnessSimulator _blindnessSimulator;

        public PaletteGenerator(IColorConverter colorConverter, IColorBlindnessSimulator blindnessSimulator)
        {
            _colorConverter = colorConverter ?? throw new ArgumentNullException(nameof(colorConverter));
            _blindnessSimulator = blindnessSimulator;
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
                case ColorBlindnessType.Monochromacy:
                    return GenerateMonochromacyPalette(n);
                case ColorBlindnessType.None:
                case ColorBlindnessType.Protanopy:
                case ColorBlindnessType.Deuteranopy:
                case ColorBlindnessType.Tritanopy:
                default:
                    return GenerateChromaticPalette(n, type);
            }
        }

        private string[] GenerateChromaticPalette(int n, ColorBlindnessType type = ColorBlindnessType.None)
        {
            var palette = new List<CIELabColor>();
            var samples = new List<CIELabColor>();
            var random = new Random();

            int nSamples = n * 100;

            for (int i = 0; i < nSamples; i++)
            {
                double l = CIELabColor.MinL + random.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MinA + random.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA);
                double b = CIELabColor.MinB + random.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);

                var color = new CIELabColor(l, a, b);

                switch (type)
                {
                    case ColorBlindnessType.Protanopy:
                        color = _blindnessSimulator.SimulateProtonapy(color);
                        break;
                    case ColorBlindnessType.Deuteranopy:
                        color = _blindnessSimulator.SimulateDeuteranopy(color);
                        break;
                    case ColorBlindnessType.Tritanopy:
                        color = _blindnessSimulator.SimulateTritanopy(color);
                        break;
                    case ColorBlindnessType.None:
                    default:
                        break;
                }

                samples.Add(color);
            }

            if (samples.Count == 0)
                throw new Exception("No valid samples found!");

            var centroids = samples.OrderBy(x => random.Next()).Take(n).ToList();
            var clusters = new List<CIELabColor>[n];
            bool changed;
            int maxIterations = 10;

            for (int iter = 0; iter < maxIterations; iter++)
            {
                changed = false;
                for (int i = 0; i < n; i++)
                    clusters[i] = new List<CIELabColor>();

                foreach (var sample in samples)
                {
                    int bestIndex = 0;
                    double bestDist = DefaultCIELabDistance(sample, centroids[0]);

                    for (int i = 1; i < n; i++)
                    {
                        double dist = DefaultCIELabDistance(sample, centroids[i]);
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            bestIndex = i;
                        }
                    }

                    clusters[bestIndex].Add(sample);
                }

                for (int i = 0; i < n; i++)
                {
                    if (clusters[i].Count == 0) continue;

                    var bestMedoid = centroids[i];
                    double bestTotalDist = double.MaxValue;

                    foreach (var candidate in clusters[i])
                    {
                        double totalDist = 0;
                        foreach (var point in clusters[i])
                            totalDist += DefaultCIELabDistance(candidate, point);

                        if (totalDist < bestTotalDist)
                        {
                            bestTotalDist = totalDist;
                            bestMedoid = candidate;
                        }
                    }

                    if (!bestMedoid.Equals(centroids[i]))
                    {
                        centroids[i] = bestMedoid;
                        changed = true;
                    }
                }

                if (!changed)
                    break;
            }

            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(centroids[i]);
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

        private double DefaultCIELabDistance(CIELabColor c1, CIELabColor c2)
        {
            return Math.Sqrt(
                Math.Pow(c1.L - c2.L, 2) +
                Math.Pow(c1.A - c2.A, 2) +
                Math.Pow(c1.B - c2.B, 2)
            );
        }
    }
}