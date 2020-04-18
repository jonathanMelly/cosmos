using System;

namespace interpreter
{
    public class WrongTypeException : InvalidCastException
    {
        public WrongTypeException(string message) : base(message)
        {
        }
    }
}