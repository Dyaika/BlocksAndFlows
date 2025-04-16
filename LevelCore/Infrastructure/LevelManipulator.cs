using LevelCore.Exceptions;
using LevelCore.Models;

namespace LevelCore.Infrastructure
{
    /// <summary>
    /// Functionality of level
    /// </summary>
    public static class LevelManipulator
    {
        /// <summary>
        /// Moves all dynamic block out of game matrix (sets their offset to -1)
        /// </summary>
        public static void Disassemble(this Level level)
        {
            foreach (var block in level.Blocks)
            {
                block.Move(-1);
            }
        }

        /// <summary>
        /// Represents level as game matrix
        /// </summary>
        /// <param name="level">Level</param>
        /// <returns>Game matrix</returns>
        /// <exception cref="BuildGameMatrixException">Impossible to build the game matrix</exception>
        public static Filter?[,] AsGameMatrix(this Level level)
        {
            var matrix = new Filter?[level.Width, level.Height];
            foreach (var block in level.Blocks)
            {
                if (!block.IsInStorage)
                {
                    var x = block.Offset % level.Width;
                    var y = block.Offset / level.Width;
                    for (var i = 0; i < block.Filters.Length; i++)
                    {
                        if (matrix[x+i, y] != null)
                        {
                            throw new BuildGameMatrixException(
                                $"Level is inconsistent, more than 1 filter in ({x+i}, {y}). Try disassemble level.");
                        }
                        matrix[x+i, y] = block.Filters[i];
                    }
                }
            }
            return matrix;
        }
    }
}