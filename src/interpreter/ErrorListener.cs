using System;
using Antlr4.Runtime;

namespace interpreter
{
    public class ErrorListener : BaseErrorListener
    {
        private bool hadError = false;

        public bool HadError => hadError;

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            Console.WriteLine("{0}: line {1}/column {2} {3}", e, line, charPositionInLine, msg);
            hadError = true;
        }
    }
}