namespace lib
{
    public class InvalidComparisonException : CosmosException
    {
        public InvalidComparisonException(string message) : base(message)
        {
        }
    }
}