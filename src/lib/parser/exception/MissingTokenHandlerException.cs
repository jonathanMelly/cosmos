using Antlr4.Runtime;

namespace lib.parser.exception
{
    public class MissingTokenHandlerException : CosmosException
    {
        public MissingTokenHandlerException(IToken token) :
            base($"{BuildParseErrorHeader(token)} Élément non pris en charge: {token.Text}")
        {
        }

        public MissingTokenHandlerException(ParserRuleContext context) :
            base(
                context,
                $"Élément non pris en charge: {context.GetChild(0).GetText()}")
        {
        }
    }
}