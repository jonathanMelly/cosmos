using System;
using System.Collections.Generic;
using System.Diagnostics;
using Antlr4.Runtime;

namespace interpreter
{
    public class ErrorListener : BaseErrorListener
    {
        private readonly bool printMessage;

        public bool HadError => Errors.Count>0;
        public List<string> Errors { get; } = new List<string>();

        public ErrorListener(bool printMessage = true)
        {
            this.printMessage = printMessage;
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            string message = $"line {line}:{charPositionInLine} {msg}";
            Errors.Add(message);

            if (printMessage)
            {
                Debug.WriteLine(message);    
            }
            
        }
    }
}