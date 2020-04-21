using System;

namespace lib
{
    public class CosmosException : Exception
    {
        public CosmosException(string message) : base(message)
        {
        }
    }
}