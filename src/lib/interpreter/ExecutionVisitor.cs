using System;
using lib.antlr;
using lib.console;
using lib.parser;
using lib.parser.visitor;

namespace lib.interpreter
{
    public class ExecutionVisitor : CosmosBaseVisitor<ExecutionContext>
    {
        private ExpressionVisitor expressionVisitor;

        private readonly Parser parser;
        private readonly VariableVisitor variableVisitor;
        private IConsole executionConsole;
        private Random random = new Random();


        public ExecutionVisitor(Parser parser)
        {
            this.parser = parser;
            variableVisitor = new VariableVisitor(parser);
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

        public ExecutionVisitor WithRandom(Random random)
        {
            this.random = random;
            return this;
        }

        public override ExecutionContext VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            expressionVisitor = new ExpressionVisitor(variableVisitor,parser,random);
            variableVisitor.ExpressionVisitor=expressionVisitor;

            var result = new ExecutionContext {Success = false};
            try
            {
                executionConsole ??= new DefaultConsole();
                base.VisitProgramme(context);
                result.Success = true;
            }
            catch (CosmosException e)
            {
                parser.ErrorListener.Error(e);
            }

            return result;

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

        public override ExecutionContext VisitBoucle(CosmosParser.BoucleContext context)
        {

            //WHILE
            if (context.expression_booleenne() != null)
            {
                while (expressionVisitor.Visit(context.expression_booleenne()).Boolean().Value)
                {
                    foreach (var instructionContext in context.instruction())
                    {
                        Visit(instructionContext);
                    }
                }
            }
            //FOR
            else
            {
                decimal iterations;
                if (context.boucle_avec_variable() != null)
                {
                    var variable = context.boucle_avec_variable().VARIABLE() != null ?
                        variableVisitor.GetVariable(context.boucle_avec_variable().VARIABLE(),context.boucle_avec_variable()) :
                        variableVisitor.Visit(context.boucle_avec_variable().variable());

                    iterations = variable.Value.Number().Value;
                }
                else
                {
                    iterations = expressionVisitor.Visit(context.expression_numerique()).Number().Value;
                }

                for (var i = 0; i < iterations; i++)
                {
                    foreach (var instructionContext in context.instruction())
                    {
                        Visit(instructionContext);
                    }

                }
            }

            return null;
        }

        public override ExecutionContext VisitAffecter(CosmosParser.AffecterContext context)
        {
            var variable = variableVisitor.Visit(context.variable());
            var newValue = variable.UpdatedTo(expressionVisitor.Visit(context.expression()));

            parser.Variables[variable.Name] = newValue;
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