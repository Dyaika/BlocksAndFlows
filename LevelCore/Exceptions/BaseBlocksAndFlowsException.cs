using System;
using System.Runtime.Serialization;

namespace LevelCore.Exceptions
{
    /// <summary>
    /// Base game exception class
    /// </summary>
    public abstract class BaseBlocksAndFlowsException: Exception
    {
        protected BaseBlocksAndFlowsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected BaseBlocksAndFlowsException(string message) : base(message)
        {
        }

        protected BaseBlocksAndFlowsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}