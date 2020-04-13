using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using interpreter.antlr;

namespace interpreter
{
    public class Interpreter
    {
        private string codeFile;
        private string code;
        private CosmosParser parser;
        private ErrorListener errorListener;

        private CosmosParser.ProgrammeContext context;

        private IConsole console;
        
        public List<string> Errors => errorListener.Errors;

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
            parser = new CosmosParser(tokens);
            
            errorListener = new ErrorListener(console);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);
            
            context = parser.programme();
            return !errorListener.HadError;
        }

        public bool Execute()
        {
            if (!Parse()) return false;
            
            var visitor = new ExecutorVisitor().WithConsole(console);
            visitor.Visit(context);

            return true;
        }
    }
}