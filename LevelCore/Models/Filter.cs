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
        
        /// <summary>
        /// Checks if filter is broken
        /// </summary>
        public bool IsBroken { get; private set; }

        /// <summary>
        /// Breaks filter
        /// </summary>
        public void Break()
        {
            IsBroken = true;
        }

        /// <summary>
        /// Fixes filter
        /// </summary>
        public void Fix()
        {
            IsBroken = false;
        }

        public Filter(byte colorId, byte shapeId)
        {
            ColorId = colorId;
            ShapeId = shapeId;
            IsBroken = false;
        }

        public Filter(Filter other)
        {
            ColorId = other.ColorId;
            ShapeId = other.ShapeId;
            IsBroken = other.IsBroken;
        }
    }
}