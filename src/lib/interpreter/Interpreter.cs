using System;
using lib.parser;

namespace lib.interpreter
{
    public class Interpreter
    {
        private readonly Parser parser;
        private Random random = new Random();

        public Interpreter(Parser parser)
        {
            this.parser = parser;
        }

        public Interpreter WithRandom(Random random)
        {
            this.random = random;
            return this;
        }

        public bool Execute()
        {
            var parseResult = parser.Parse();
            //Something went wrong in parsing ?
            if (parseResult==false) return false;

            var visitor = new ExecutionVisitor(parser).WithRandom(random);
            var result = visitor.Visit(parser.Context);

            return result.Success;
        }
    }
}