using System.Linq;

namespace LevelCore
{
    /// <summary>
    /// Block of filters
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Offset from 0;0 coordinate (bottom left) in game matrix
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Type of block
        /// </summary>
        public BlockType Type { get; }

        /// <summary>
        /// Check if block is static (not dynamic)
        /// </summary>
        public bool IsStatic { get; }

        /// <summary>
        /// Collection of filters
        /// </summary>
        public Filter[] Filters => _filters.ToArray();

        private readonly Filter[] _filters;

        public Block(Filter[] filters, int offset, BlockType type, bool isStatic)
        {
            _filters = filters;
            Offset = offset;
            Type = type;
            IsStatic = isStatic;
        }
    }
}