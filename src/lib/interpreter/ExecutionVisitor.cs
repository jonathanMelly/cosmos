using System;
using System.Threading;
using lib.antlr;
using lib.console;
using lib.extension;
using lib.parser;
using lib.parser.exception;
using lib.parser.type;
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

            var variable = context.variable() != null ?
                variableVisitor.Visit(context.variable()) :
                variableVisitor.GetVariable(context.la_zone_memoire().VARIABLE(), context.la_zone_memoire());

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

        public override ExecutionContext VisitRecuperer(CosmosParser.RecupererContext context)
        {
            var input = executionConsole.ReadLine();
            CosmosTypedValue typedValue;

            if (bool.TryParse(input.Replace("vrai","true").Replace("faux","false"),out var boolResult))
            {
                typedValue = boolResult.AsCosmosBoolean();
            }
            else if (decimal.TryParse(input, out var numberResult))
            {
                typedValue = numberResult.AsCosmosNumber();
            }
            else
            {
                typedValue = input.AsCosmosString();
            }
            var variable = variableVisitor.Visit(context.la_zone_memoire());

            parser.Variables[variable.Name] = variable.UpdatedTo(typedValue);

            return null;
        }

        public override ExecutionContext VisitGenerer_aleatoire(CosmosParser.Generer_aleatoireContext context)
        {
            var min = Convert.ToInt32(expressionVisitor.Visit(context.min).Number().Value);
            var max = Convert.ToInt32(expressionVisitor.Visit(context.max).Number().Value);

            var variable = variableVisitor.GetVariable(context.la_zone_memoire().VARIABLE(), context.la_zone_memoire());

            var newValue = variable.UpdatedTo(new CosmosNumber(random.Next(min,max+1)));

            parser.Variables[variable.Name] = newValue;

            return null;
        }

        public override ExecutionContext VisitPlacer_curseur(CosmosParser.Placer_curseurContext context)
        {
            var index = (int) expressionVisitor.Visit(context.expression_numerique()).Number().Value;
            if (context.ligne != null)
            {
                executionConsole.SetCursorToLine(index);
            }
            else
            {
                executionConsole.SetCursorToColumn(index);
            }

            return null;
        }

        public override ExecutionContext VisitDormir(CosmosParser.DormirContext context)
        {
            var time = (int) expressionVisitor.Visit(context.expression_numerique()).Number().Value;
            Thread.Sleep(time);
            return null;
        }

        public override ExecutionContext VisitColorier(CosmosParser.ColorierContext context)
        {
            var dark ="";
            var translatedColor = "";

            if (context.dark != null)
            {
                dark = "Dark";
            }

            if (context.blue != null)
            {
                translatedColor = "Blue";
            }
            else if (context.green != null)
            {
                translatedColor = "Green";
            }
            else if (context.red != null)
            {
                translatedColor = "Red";
            }
            else if (context.white != null)
            {
                translatedColor = "White";
            }
            else if (context.black!=null)
            {
                translatedColor = "Black";
            }
            else
            {
                throw new UnHandledColorException("La couleur indiquée n'est pas encore prise en charge");
            }

            var newColor = ConsoleColor.Gray;
            bool parse = ConsoleColor.TryParse($"{dark}{translatedColor}",out newColor);

            if (parse)
            {
                if (context.background!=null)
                {
                    executionConsole.SetBackColorTo(newColor.ToString());
                }
                else
                {
                    executionConsole.SetFrontColorTo(newColor.ToString());
                }
            }
            return null;
        }
    }
}