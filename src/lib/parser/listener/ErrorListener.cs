using System.Collections.Generic;
using Antlr4.Runtime;
using lib.console;

namespace lib.parser.listener
{
    public class ErrorListener : BaseErrorListener
    {
        private readonly IConsole console;

        public ErrorListener(IConsole console = null)
        {
            this.console = console ?? new DefaultConsole();
        }

        public bool HadError => Errors.Count > 0;
        public List<string> Errors { get; } = new List<string>();

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line,
            int charPositionInLine, string msg,
            RecognitionException e)
        {
            Error(line, charPositionInLine, msg);
        }

        public void Error(CosmosException exception)
        {
            Error(exception.Message);
        }

        public void Error(string message)
        {
            string finalMessage = Translate(message);
            Errors.Add(finalMessage);
            console.WriteLine(finalMessage, IConsole.Channel.Error);
        }

        public void Error(int line, int column, string message)
        {
            var finalMessage = $"{CosmosException.BuildParseErrorHeader(line,column)} {message}";
            Error(finalMessage);
        }

        private string Translate(string message)
        {
            return message.
                Replace(" at ", " à l'endroit ou il y a ").
                Replace("mismatched input", "élément invalide").
                Replace("expecting", "attendu").
                Replace("missing", "il manque").
                Replace("extraneous input","élément inconnu");
        }
    }
}