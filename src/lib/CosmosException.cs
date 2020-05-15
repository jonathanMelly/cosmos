using System;
using Antlr4.Runtime;

namespace lib
{
    public class CosmosException : Exception
    {

        public CosmosException(string message) : base(message)
        {
        }

        public static string BuildParseErrorHeader(ParserRuleContext context)
        {
            return BuildParseErrorHeader(context.start);
        }

        public static string BuildParseErrorHeader(IToken token)
        {
            return BuildParseErrorHeader(token.Line,token.Column);
        }

        public static string BuildParseErrorHeader(int line,int column)
        {
            return $"Erreur, ligne {line}:{column}";
        }

    }
}