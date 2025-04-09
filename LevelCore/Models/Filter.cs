namespace LevelCore.Models
{
    /// <summary>
    /// Filter for particles
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Color identificator
        /// </summary>
        public byte ColorId { get; }

        /// <summary>
        /// Shape identificator
        /// </summary>
        public byte ShapeId { get; }

        public Filter(byte colorId, byte shapeId)
        {
            ColorId = colorId;
            ShapeId = shapeId;
        }

        public Filter(Filter other)
        {
            ColorId = other.ColorId;
            ShapeId = other.ShapeId;
        }
    }
}