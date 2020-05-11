using System;

namespace lib.parser.exception
{
    public class WrongTypeException : InvalidCastException
    {
        public WrongTypeException(string message) : base(message)
        {
        }
    }
}