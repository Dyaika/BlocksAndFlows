namespace LevelCore.Models
{
    /// <summary>
    /// Result after simulating flows through filters
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Game matrix with broken filters
        /// </summary>
        public Filter[,] Matrix => _matrix;

        /// <summary>
        /// Calculated score
        /// </summary>
        public float Score { get; }

        private Filter[,] _matrix;

        public Result(Filter[,] matrix, float score)
        {
            _matrix = matrix;
            Score = score;
        }
    }
}