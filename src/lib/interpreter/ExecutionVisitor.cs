using lib.antlr;
using lib.console;
using lib.parser;
using lib.parser.visitor;

namespace lib.interpreter
{
    public class ExecutionVisitor : CosmosBaseVisitor<ExecutionContext>
    {
        private readonly ExpressionVisitor expressionVisitor;

        private readonly Parser parser;
        private readonly VariableVisitor variableVisitor;
        private IConsole executionConsole;


        public ExecutionVisitor(Parser parser)
        {
            this.parser = parser;
            variableVisitor = new VariableVisitor(parser);
            expressionVisitor = new ExpressionVisitor(variableVisitor);
            variableVisitor.ExpressionVisitor=expressionVisitor;
        }

        /// <summary>
        ///     Set customized output
        /// </summary>
        /// <param name="console"></param>
        public ExecutionVisitor WithConsole(IConsole console)
        {
            executionConsole = console;
            return this;
        }

        public override ExecutionContext VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            executionConsole ??= new DefaultConsole();
            return base.VisitProgramme(context);
        }

        public override ExecutionContext VisitSelection(CosmosParser.SelectionContext context)
        {
            var evaluation = expressionVisitor.Visit(context.base_si().condition);
            if (evaluation.Boolean().Value)
                foreach (var instructionIntegree in context.base_si().instruction())
                    Visit(instructionIntegree);
            else if (context.sinon_si() != null)
                foreach (var elsif in context.sinon_si())
                    if (expressionVisitor.Visit(elsif.base_si().condition).Boolean().Value)
                    {
                        foreach (var instructionIntegree in elsif.base_si().instruction()) Visit(instructionIntegree);
                        return null; //only 1 elsif branch
                    }

            if (context.sinon() != null)
                foreach (var instructionIntegree in context.sinon().instruction())
                    Visit(instructionIntegree);

            return null;
        }

        public override ExecutionContext VisitAfficher(CosmosParser.AfficherContext context)
        {
            executionConsole.Write(expressionVisitor.Visit(context.expression()).ToString());

            return null;
        }

        public override ExecutionContext VisitAllouer(CosmosParser.AllouerContext context)
        {
            var variable = variableVisitor.Visit(context);
            parser.Variables[variable.Name] = variable;

            return null;
        }
    }
}