using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using interpreter.antlr;

namespace interpreter
{
    public class Interpreter
    {
        private string code;
        private string codeFile;

        private IConsole console;

        private CosmosParser.ProgrammeContext context;

        //Keep redirection because we may have more listeners in the future...
        public List<string> Errors => ErrorListener.Errors;

        public IDictionary<string, CosmosVariable> Variables { get; } = new Dictionary<string, CosmosVariable>();

        public ErrorListener ErrorListener { get; private set; }

        public Interpreter ForFile(string file)
        {
            codeFile = file;
            code = File.ReadAllText(file);
            return this;
        }

        public Interpreter ForSnippet(string snippet)
        {
            code = snippet;
            return this;
        }

        public Interpreter WithConsole(IConsole console)
        {
            this.console = console;
            return this;
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
            return !ErrorListener.HadError;
        }

        public bool Execute()
        {
            if (!Parse()) return false;

            var visitor = new ExecutionVisitor(this).WithConsole(console);
            visitor.Visit(context);

            return true;
        }
    }
}