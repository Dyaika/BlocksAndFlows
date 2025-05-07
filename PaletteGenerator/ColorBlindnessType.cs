namespace PaletteGenerator
{
    /// <summary>
    /// Types of color blindness
    /// </summary>
    public enum ColorBlindnessType : byte
    {
        /// <summary>
        /// No color blindness
        /// </summary>
        None = 0,

        /// <summary>
        /// Protanopy (protanopia) - lack of red color perception
        /// </summary>
        Protanopy = 1,

        /// <summary>
        /// Deuteranopy (deuteranopia) - lack of green color perception
        /// </summary>
        Deuteranopy = 2,

        /// <summary>
        /// Tritanopy (tritanopia) - lack of blue color perception
        /// </summary>
        Tritanopy = 3,

        /// <summary>
        /// Monochromacy - complete color blindness, perception of only shades of gray
        /// </summary>
        Monochromacy = 4
    }
}