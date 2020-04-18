namespace interpreter
{
    public class InvalidComparisonException : CosmosException
    {
        public InvalidComparisonException(string message) : base(message)
        {
        }
    }
}