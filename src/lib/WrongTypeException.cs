using System;

namespace lib
{
    public class WrongTypeException : InvalidCastException
    {
        public WrongTypeException(string message) : base(message)
        {
        }
    }
}