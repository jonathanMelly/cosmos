using lib.console;
using lib.parser;

namespace lib.interpreter
{
    public class Interpreter
    {
        private readonly Parser parser;
        private IConsole console;
        
        public Interpreter(Parser parser, IConsole console=null)
        {
            this.parser = parser;
            this.console = console;
        }
        
        public bool Execute()
        {
            if (!parser.Parse()) return false;

            var visitor = new ExecutionVisitor(parser).WithConsole(console);
            visitor.Visit(parser.Context);

            return true;
        }
    }
}