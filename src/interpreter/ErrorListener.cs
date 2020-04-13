using System.Collections.Generic;
using Antlr4.Runtime;

namespace interpreter
{
    public class ErrorListener : BaseErrorListener
    {
        private readonly IConsole console;

        public bool HadError => Errors.Count>0;
        public List<string> Errors { get; } = new List<string>();

        public ErrorListener(IConsole console=null)
        {
            this.console = console ?? new DefaultConsole();
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            string message = $"line {line}:{charPositionInLine} {msg}";
            Errors.Add(message);
            
            console.WriteLine(message,IConsole.Channel.Error);

        }
    }
}