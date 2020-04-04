using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using interpreter.antlr;

namespace interpreter
{
    public class Interpreter
    {
        private readonly CosmosParser parser;
        private readonly ErrorListener errorListener;

        private CosmosParser.ProgrammeContext context;
        

        public Interpreter(string file)
        {
            var code = File.ReadAllText(file);
            
            var antlrInputStream = new AntlrInputStream(code);
            var lexer = new CosmosLexer(antlrInputStream);
            var tokens = new CommonTokenStream(lexer);
            parser = new CosmosParser(tokens);
            
            errorListener = new ErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);
            
        }

        public List<string> Errors => errorListener.Errors;

        public bool Parse()
        {
            context = parser.programme();
            return !errorListener.HadError;
        }

        public void Execute()
        {
            if (!Parse()) return;
            
            var visitor = new ExecutorVisitor();
            visitor.Visit(context);
        }
    }
}