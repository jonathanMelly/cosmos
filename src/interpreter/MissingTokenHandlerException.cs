using Antlr4.Runtime;

namespace interpreter
{
    public class MissingTokenHandlerException : CosmosException
    {
        public MissingTokenHandlerException(IToken token) :
            base($"ligne {token.Line}:{token.Column} No handler for token {token.Text}")
        {
        }

        public MissingTokenHandlerException(ParserRuleContext context) :
            base(
                $"ligne {context.Start.Line}:{context.Start.Column} No handler for token {context.GetChild(0).GetText()}")
        {
        }
    }
}