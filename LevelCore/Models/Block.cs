using System.Linq;

namespace LevelCore.Models
{
    /// <summary>
    /// Block of filters
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Offset from 0;0 coordinate (bottom left) in game matrix
        /// </summary>
        public int Offset => _offset;

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

        /// <summary>
        /// Check if block is in storage or on the game field
        /// </summary>
        public bool IsInStorage => _offset == -1;

        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        private readonly Filter[] _filters;
        private int _offset;

        public Block(Filter[] filters, int offset, BlockType type, bool isStatic)
        {
            _filters = filters;
            _offset = offset;
            Type = type;
            IsStatic = isStatic;
        }

        /// <summary>
        /// Changes offset
        /// </summary>
        /// <param name="offset">New offset</param>
        /// <returns>Was moved</returns>
        public bool Move(int offset)
        {
            if (offset < 0)
            {
                offset = -1;
            }

            if (IsStatic || offset == _offset)
            {
                return false;
            }

            _offset = offset;
            return true;
        }
    }
}