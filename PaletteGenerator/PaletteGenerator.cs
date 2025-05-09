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
                    return GenerateRepelledPoints(n, DefaultCIELabDistance);
                case ColorBlindnessType.Protanopy:
                    return GenerateRepelledPoints(n, ProtanopyDistance);
                case ColorBlindnessType.Deuteranopy:
                    return GenerateRepelledPoints(n, DeuteranopyDistance);
                case ColorBlindnessType.Tritanopy:
                    return GenerateRepelledPoints(n, TritanopyDistance);
                default:
                    return GenerateChromaticPalette(n, type);
            }
        }

        private string[] GenerateChromaticPalette(int n, ColorBlindnessType type = ColorBlindnessType.None)
        {
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

        public string[] GenerateRepelledPoints(
            int n,
            Func<CIELabColor, CIELabColor, double> distanceFunc,
            int iterations = 100,
            double stepSize = 1.0,
            double boundaryRepel = 10.0)
        {
            var rand = new Random();
            var points = new List<CIELabColor>();

            for (int i = 0; i < n; i++)
            {
                
                double l = CIELabColor.MinL + rand.NextDouble() * (CIELabColor.MaxL - CIELabColor.MinL);
                double a = CIELabColor.MinA + rand.NextDouble() * (CIELabColor.MaxA - CIELabColor.MinA);
                double b = CIELabColor.MinB + rand.NextDouble() * (CIELabColor.MaxB - CIELabColor.MinB);
                
                points.Add(new CIELabColor(l, a, b));
            }

            for (int iter = 0; iter < iterations; iter++)
            {
                var deltas = new (double dL, double dA, double dB)[n];

                for (int i = 0; i < n; i++)
                {
                    double dL = 0, dA = 0, dB = 0;
                    var pi = points[i];

                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;
                        var pj = points[j];

                        double dist = distanceFunc(pi, pj);
                        double dx = pi.L - pj.L;
                        double dy = pi.A - pj.A;
                        double dz = pi.B - pj.B;

                        double distSq = dist * dist + 1e-6;
                        double force = 1.0 / distSq;

                        dL += dx * force;
                        dA += dy * force;
                        dB += dz * force;
                    }

                    dL += (pi.L < 5 ? boundaryRepel : 0) - (pi.L > 95 ? boundaryRepel : 0);
                    dA += (pi.A < -120 ? boundaryRepel : 0) - (pi.A > 120 ? boundaryRepel : 0);
                    dB += (pi.B < -120 ? boundaryRepel : 0) - (pi.B > 120 ? boundaryRepel : 0);

                    deltas[i] = (dL, dA, dB);
                }

                for (int i = 0; i < n; i++)
                {
                    var p = points[i];
                    var (dL, dA, dB) = deltas[i];

                    points[i] = new CIELabColor
                    (
                        Clamp(p.L + dL * stepSize, 0, 100),
                        Clamp(p.A + dA * stepSize, -128, 127),
                        Clamp(p.B + dB * stepSize, -128, 127)
                    );
                }
            }

            
            var hexPalette = new string[n];
            for (int i = 0; i < n; i++)
            {
                var rgb = _colorConverter.CIELabToSRGB(points[i]);
                hexPalette[i] = _colorConverter.sRGBToHex(rgb);
            }

            return hexPalette;
        }

        private static double Clamp(double x, double min, double max)
        {
            return Math.Max(min, Math.Min(max, x));
        }

        private double DefaultCIELabDistance(CIELabColor c1, CIELabColor c2)
        {
            return Math.Sqrt(
                Math.Pow(c1.L - c2.L, 2) +
                Math.Pow(c1.A - c2.A, 2) +
                Math.Pow(c1.B - c2.B, 2)
            );
        }
        
        private double ProtanopyDistance(CIELabColor c1, CIELabColor c2)
        {
            c1 = _blindnessSimulator.SimulateProtonapy(c1);
            c2 = _blindnessSimulator.SimulateProtonapy(c2);
            return DefaultCIELabDistance(c1, c2);
        }
        private double DeuteranopyDistance(CIELabColor c1, CIELabColor c2)
        {
            c1 = _blindnessSimulator.SimulateDeuteranopy(c1);
            c2 = _blindnessSimulator.SimulateDeuteranopy(c2);
            return DefaultCIELabDistance(c1, c2);
        }
        private double TritanopyDistance(CIELabColor c1, CIELabColor c2)
        {
            c1 = _blindnessSimulator.SimulateTritanopy(c1);
            c2 = _blindnessSimulator.SimulateTritanopy(c2);
            return DefaultCIELabDistance(c1, c2);
        }
    }
}