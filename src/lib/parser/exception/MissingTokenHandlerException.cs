using Antlr4.Runtime;

namespace lib.parser.exception
{
    public class MissingTokenHandlerException : CosmosException
    {
        public MissingTokenHandlerException(IToken token) :
            base($"{BuildParseErrorHeader(token)} Element non pris en charge: {token.Text}")
        {
        }

        public MissingTokenHandlerException(ParserRuleContext context) :
            base(
                $"{BuildParseErrorHeader(context)} {context.Start.Line}:{context.Start.Column} " +
                $"Element non pris en charge: {context.GetChild(0).GetText()}")
        {
        }
    }
}