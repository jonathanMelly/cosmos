using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using lib.antlr;
using lib.console;
using lib.parser.listener;
using lib.parser.type;

namespace lib.parser
{
    public class Parser
    {
        public const string ValidHeaderSnippet = "Auteur: Jonathan Strokee\n" +
                                                  "Date: 27.03.2020\n" +
                                                  "Entreprise: ETML\n" +
                                                  "Description: Demonstration du langage cosmos,\n" +
                                                  "ce langage esttodo extraordinaire\n" +
                                                  "Voici les ordres du programme DEMO_COSMOS à classer dans la bibliothèque DEMONSTRATION :\n";

        public const string ValidEnd = "\nFin de la transmission.";

        private string code;
        private string codeFile;

        private IConsole console;

        private CosmosParser.ProgrammeContext context;

        //Keep redirection because we may have more listeners in the future...
        public List<string> Errors => ErrorListener.Errors;

        public IDictionary<string, CosmosVariable> Variables { get; } = new Dictionary<string, CosmosVariable>();

        public ErrorListener ErrorListener { get; private set; }

        public CosmosParser.ProgrammeContext Context => context;

        public Parser ForFile(string file)
        {
            codeFile = file;
            code = File.ReadAllText(file);
            return this;
        }

        public Parser ForSnippet(string snippet,bool addHeader = false)
        {
            code = $"{(addHeader?ValidHeaderSnippet:"")}{snippet}{ValidEnd}";
            return this;
        }

        public Parser WithConsole(IConsole console)
        {
            this.console = console;
            return this;
        }

        public void CopyContext(Parser parser)
        {
            foreach (var existingVariable in parser.Variables)
            {
                Variables.Add(existingVariable);
            }
        }

        public bool Parse()
        {
            var antlrInputStream = new AntlrInputStream(code);
            var lexer = new CosmosLexer(antlrInputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CosmosParser(tokens);

            ErrorListener = new ErrorListener(console);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(ErrorListener);

            context = parser.programme();

            //TODO : add variablecheckerVisitor ;-)

            return !ErrorListener.HadError;
        }
    }
}