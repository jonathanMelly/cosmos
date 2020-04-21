namespace lib.parser.exception
{
    public class InvalidComparisonException : CosmosException
    {
        public InvalidComparisonException(string message) : base(message)
        {
        }
    }
}