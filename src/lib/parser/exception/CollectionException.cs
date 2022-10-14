using Antlr4.Runtime;

namespace lib.parser.exception
{
    public class CollectionException : CosmosException
    {
        public CollectionException(string message) : base(message)
        {
        }

        public CollectionException(ParserRuleContext context, string specificMessage) : base(context, specificMessage)
        {
        }
    }
}