namespace LevelCore
{
    /// <summary>
    /// Structure of game level
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Width of game matrix
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of game matrix
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Collection of blocks
        /// </summary>
        public Block[] Blocks { get; }

        public Level(int width, int height, Block[] blocks)
        {
            Width = width;
            Height = height;
            Blocks = blocks;
        }
    }
}