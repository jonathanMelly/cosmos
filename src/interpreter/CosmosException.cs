using System;

namespace interpreter
{
    public class CosmosException : Exception
    {
        public CosmosException(string message) : base(message)
        {
        }
    }
}