using System;
using lib.console;
using lib.parser;

namespace lib.interpreter
{
    public class Interpreter
    {
        private readonly Parser parser;
        private IConsole console;
        private Random random = new Random();

        public Interpreter(Parser parser, IConsole console=null)
        {
            this.parser = parser;
            this.console = console;
        }

        public Interpreter WithRandom(Random random)
        {
            this.random = random;
            return this;
        }

        public bool Execute()
        {
            if (!parser.Parse()) return false;

            var visitor = new ExecutionVisitor(parser,console).WithRandom(random);
            var result = visitor.Visit(parser.Context);

            return result.Success;
        }
    }
}