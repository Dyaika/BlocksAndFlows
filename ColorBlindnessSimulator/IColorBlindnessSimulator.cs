﻿using ColorSpaces.Spaces;

namespace ColorBlindnessSimulator
{
    public interface IColorBlindnessSimulator
    {
        CIELabColor SimulateProtonapy(CIELabColor color);
        CIELabColor SimulateDeuteranopy(CIELabColor color);
        CIELabColor SimulateTritanopy(CIELabColor color);
    }
}