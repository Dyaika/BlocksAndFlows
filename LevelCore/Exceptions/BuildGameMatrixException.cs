using System;
using System.Runtime.Serialization;

namespace LevelCore.Exceptions
{
    /// <summary>
    /// Exception for inconsistent levels
    /// </summary>
    public class BuildGameMatrixException: BaseBlocksAndFlowsException
    {
        public BuildGameMatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BuildGameMatrixException(string message) : base(message)
        {
        }

        public BuildGameMatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}