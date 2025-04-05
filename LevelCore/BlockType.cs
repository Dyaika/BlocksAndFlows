namespace LevelCore
{
    /// <summary>
    /// Type of block
    /// </summary>
    public enum BlockType : byte
    {
        /// <summary>
        /// Can produce particles
        /// </summary>
        Producer = 0,

        /// <summary>
        /// Can consume particles and convert them, producing new ones
        /// </summary>
        Converter = 1,

        /// <summary>
        /// Can receive particles
        /// </summary>
        Receiver = 2
    }
}