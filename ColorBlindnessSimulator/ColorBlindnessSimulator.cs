using ColorSpaces.Converters;
using ColorSpaces.Spaces;

namespace ColorBlindnessSimulator
{
    public class ColorBlindnessSimulator: IColorBlindnessSimulator
    {
        private IColorConverter _converter;

        public ColorBlindnessSimulator(IColorConverter converter)
        {
            _converter = converter;
        }

        public CIELabColor SimulateProtonapy(CIELabColor color)
        {
            var lms = _converter.CIELabToLMS(color);

            var matrix = new double[,]
            {
                { 0.0, 1.05118294, -0.05116099 },
                { 0.0, 1.0,         0.0        },
                { 0.0, 0.0,         1.0        }
            };

            return _converter.LMSToCIELab(SimulateInLMS(matrix, lms));
        }

        public CIELabColor SimulateDeuteranopy(CIELabColor color)
        {
            var lms = _converter.CIELabToLMS(color);

            var matrix = new double[,]
            {
                { 1.0,       0.0, 0.0        },
                { 0.9513092, 0.0, 0.04866992 },
                { 0.0,       0.0, 1.0        }
            };

            return _converter.LMSToCIELab(SimulateInLMS(matrix, lms));
        }

        public CIELabColor SimulateTritanopy(CIELabColor color)
        {
            var lms = _converter.CIELabToLMS(color);

            var matrix = new double[,]
            {
                { 1.0,         0.0,        0.0 },
                { 0.0,         1.0,        0.0 },
                {-0.86744736, 1.86727089,  0.0 }
            };

            return _converter.LMSToCIELab(SimulateInLMS(matrix, lms));
        }

        private LMSColor SimulateInLMS(double[,] matrix, LMSColor color)
        {
            double l = matrix[0, 0] * color.L + matrix[0, 1] * color.M + matrix[0, 2] * color.S;
            double m = matrix[1, 0] * color.L + matrix[1, 1] * color.M + matrix[1, 2] * color.S;
            double s = matrix[2, 0] * color.L + matrix[2, 1] * color.M + matrix[2, 2] * color.S;

            return new LMSColor(l, m, s);
        }
    }
}