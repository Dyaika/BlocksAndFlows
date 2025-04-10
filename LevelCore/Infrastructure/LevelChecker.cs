using System;
using LevelCore.Models;

namespace LevelCore.Infrastructure
{
    public static class LevelChecker
    {
        /// <summary>
        /// Simulates flows and breaks wrong placed filters in blocks
        /// </summary>
        /// <param name="level">Level</param>
        /// <returns></returns>
        public static Filter?[,] Simulate(this Level level)
        {
            var matrix = level.AsGameMatrix();
            var checkerRow = new Filter?[level.Width];
            
            // Put top row into the checker row
            for (var col = 0; col < level.Width; col++)
            {
                checkerRow[col] = new Filter(matrix[col, level.Height - 1]);
            }

            for (var row = level.Height - 2; row >= 0; row--)
            {
                for (var col = 0; col < level.Width; col++)
                {
                    if (matrix[col, row] == null)
                    {
                        continue;
                    }

                    if (checkerRow[col].CheckFlow(matrix[col, row]))
                    {
                        checkerRow[col] = new Filter(matrix[col, row]);
                    }
                    else
                    {
                        checkerRow[col].Break();
                    }
                }
            }
            return matrix;
        }

        /// <summary>
        /// Calculates score
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static float CalculateScore(this Level level)
        {
            float N;
            float Nf;
            float R;
            float W = level.Width;
            throw new NotImplementedException();
        }

        private static bool CheckFlow(this Filter producer, Filter receiver)
        {
            return !producer.IsBroken && (producer.ColorId == receiver.ColorId || producer.ShapeId == receiver.ShapeId);
        }
    }
}