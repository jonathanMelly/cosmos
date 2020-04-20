using System.Collections.Generic;
using Antlr4.Runtime;

namespace interpreter
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

        public void UnknownVariableError(int line, int column, string variableNane)
        {
            Error(line, column,
                $"Espace mémoire {variableNane} inconnu. Il manque probablement la ligne : Allouer {variableNane}.");
        }

        public void Error(int line, int column, string message)
        {
            var finalMessage = $"ligne {line}:{column} {Translate(message)}";
            Errors.Add(finalMessage);
            console.WriteLine(finalMessage, IConsole.Channel.Error);
        }

        private string Translate(string message)
        {
            return message.
                Replace("mismatched input", "élément invalide").
                Replace("expecting", "attendu");
        }
    }
}