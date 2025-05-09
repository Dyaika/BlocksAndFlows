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
        /// <returns>Game matrix with broken filters</returns>
        public static Filter[,] SimulateAsMatrix(this Level level)
        {
            var matrix = level.AsGameMatrix();
            var checkerRow = new Filter[level.Width];

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
                        matrix[col, row].Break();
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// Calculates score
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Score in %</returns>
        public static Result CalculateResult(this Level level)
        {
            var simulatedMatrix = level.SimulateAsMatrix();
            float D = 0; // number of filters in [D]ynamic blocks
            float B = 0; // number of [B]roken filters in dynamic blocks, or dynamic blocks in storage
            float R = 0; // number of flows [R]eceived by bottom receivers
            float P = 0; // number of flows [P]roduced by top producers

            // Calculate R
            for (var col = 0; col < level.Width; col++)
            {
                if (!simulatedMatrix[col, 0].IsBroken)
                {
                    R++;
                }
            }

            // Calculate D and P, partialy B
            foreach (var block in level.Blocks)
            {
                if (!block.IsStatic)
                {
                    D += block.Filters.Length;

                    if (block.IsInStorage)
                    {
                        B += block.Filters.Length;
                    }
                }

                if (block.Type == BlockType.Producer)
                {
                    P += block.Filters.Length;
                }
            }

            // Calculate B
            for (var row = 1; row < level.Height - 1; row++)
            {
                for (var col = 0; col < level.Width; col++)
                {
                    if (simulatedMatrix[col, row] != null && simulatedMatrix[col, row].IsBroken)
                    {
                        B++;
                    }
                }
            }

            var score = (D - B) / D * (R / P) * 100.0f;
            return new Result(simulatedMatrix, score);
        }

        private static bool CheckFlow(this Filter producer, Filter receiver)
        {
            return !producer.IsBroken && (producer.ColorId == receiver.ColorId || producer.ShapeId == receiver.ShapeId);
        }
    }
}