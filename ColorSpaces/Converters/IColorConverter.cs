using ColorSpaces.Spaces;

namespace ColorSpaces.Converters
{
    public interface IColorConverter
    {
        /// <summary>
        /// Converts sRGB to hex string
        /// </summary>
        /// <param name="color">Color in sRGB</param>
        /// <returns>Color as hex string</returns>
        string sRGBToHex(RGBColor color);

        /// <summary>
        /// Converts linear RGB to sRGB
        /// </summary>
        /// <param name="color">Color in linear RGB</param>
        /// <returns>Color in sRGB</returns>
        RGBColor LinearRGBToSRGB(RGBColor color);

        /// <summary>
        /// Converts sRGB to linear RGB
        /// </summary>
        /// <param name="color">Color in sRGB</param>
        /// <returns>Color in linear RGB</returns>
        RGBColor SRGBToLinearRGB(RGBColor color);

        /// <summary>
        /// Converts sRGB to XYZ
        /// </summary>
        /// <param name="color">Color in sRGB</param>
        /// <returns>Color in XYZ</returns>
        XYZColor SRGBToXYZ(RGBColor color);

        /// <summary>
        /// Converts XYZ to sRGB
        /// </summary>
        /// <param name="color">Color in XYZ</param>
        /// <returns>Color in sRGB</returns>
        RGBColor XYZToSRGB(XYZColor color);

        /// <summary>
        /// Converts CIE Lab to sRGB
        /// </summary>
        /// <param name="color">Color in CIE Lab</param>
        /// <returns>Color in sRGB</returns>
        RGBColor CIELabToSRGB(CIELabColor color);

        /// <summary>
        /// Converts sRGB to CIE Lab
        /// </summary>
        /// <param name="color">Color in sRGB</param>
        /// <returns>Color in CIE Lab</returns>
        CIELabColor SRGBToCIELab(RGBColor color);

        /// <summary>
        /// Converts LMS to XYZ
        /// </summary>
        /// <param name="color">Color in LMS</param>
        /// <returns>Color in XYZ</returns>
        XYZColor LMStoXYZ(LMSColor color);

        /// <summary>
        /// Converts XYZ to LMS
        /// </summary>
        /// <param name="color">Color in XYZ</param>
        /// <returns>Color in LMS</returns>
        LMSColor XYZToLMS(XYZColor color);

        /// <summary>
        /// Converts XYZ to CIE Lab
        /// </summary>
        /// <param name="color">Color in XYZ</param>
        /// <returns>Color in CIE Lab</returns>
        CIELabColor XYZToCIELab(XYZColor color);

        /// <summary>
        /// Converts CIE Lab to XYZ
        /// </summary>
        /// <param name="color">Color in CIE Lab</param>
        /// <returns>Color in XYZ</returns>
        XYZColor CIELabToXYZ(CIELabColor color);

        /// <summary>
        /// Converts CIE Lab to LMS
        /// </summary>
        /// <param name="color">Color in CIE Lab</param>
        /// <returns>Color in LMS</returns>
        LMSColor CIELabToLMS(CIELabColor color);

        /// <summary>
        /// Converts LMS to CIE Lab
        /// </summary>
        /// <param name="color">Color in LMS</param>
        /// <returns>Color in CIE Lab</returns>
        CIELabColor LMSToCIELab(LMSColor color);
    }
}