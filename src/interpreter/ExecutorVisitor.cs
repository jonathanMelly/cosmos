using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<CosmosData>
    {
        private IConsole executionConsole;
        private const char StringDelimiter = '\"';
        
        
        /// <summary>
        /// Set customized output
        /// </summary>
        /// <param name="console"></param>
        public ExecutorVisitor WithConsole(IConsole console)
        {
            this.executionConsole = console;
            return this;
        }

        public override CosmosData VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            this.executionConsole = executionConsole ?? new DefaultConsole();
            return base.VisitProgramme(context);
        }

        public override CosmosData VisitSelection(CosmosParser.SelectionContext context)
        {
            CosmosData result = new CosmosData();
            
            var evaluation = ComputeCondition(context.si);
            if (evaluation.Item2)
            {
                foreach (var instructionIntegree in context.instruction())
                {
                    AggregateResult(result,Visit(instructionIntegree));
                }
            }
            else if (context.sinon_si() != null)
            {
                foreach (var elsif in context.sinon_si())
                {
                    if(ComputeCondition(elsif.condition()).Item2)
                    {
                        foreach (var instructionIntegree in elsif.instruction())
                        {
                            AggregateResult(result,(Visit(instructionIntegree)));
                        }
                        return result;//only 1 elsif branch
                    }
                }
            }
            
            if (context.sinon() != null)
            {
                foreach (var instructionIntegree in context.sinon().instruction())
                {
                    AggregateResult(result,(Visit(instructionIntegree)));
                }
            }

            return result;
        }

        private Tuple<string,bool> ComputeCondition(CosmosParser.ConditionContext context)
        {
            var left = ComputeValue(context.left);
            var right = ComputeValue(context.right);

            var test = GetLexerType(context.operateur_comparaison()) switch
            {
                CosmosLexer.OPERATEUR_EGAL => left.Equals(right),
                CosmosLexer.OPERATEUR_DIFFERENT => !left.Equals(right),
                _ => false
            };

            return Tuple.Create("if ("+left+"=="+right+"){",test);
        }

        private int GetLexerType(ParserRuleContext parserRuleContext)
        {
            if (parserRuleContext == null) throw new ArgumentNullException(nameof(parserRuleContext));
            return ((ITerminalNode) parserRuleContext.GetChild(0)).Symbol.Type;
        }

        private object ComputeValue(CosmosParser.Expression_valeurContext context)
        {
            CosmosParser.Expression_numeraireContext numericContext;
            if ((numericContext = context.expression_numeraire()) != null)
            {
                //TODO : types plus fins ?
                return Convert.ToDecimal(numericContext.VALEUR_NOMBRE().ToString());
            }

            return context.expression_textuelle().VALEUR_TEXTE().ToString();
        }

        public override CosmosData VisitAfficher(CosmosParser.AfficherContext context)
        {
            
            string content;
            var valueContext = context.expression_valeur();
            if (valueContext.expression_textuelle() != null)
            {
                content = valueContext.expression_textuelle().VALEUR_TEXTE().ToString().TrimStart(StringDelimiter).TrimEnd(StringDelimiter);
                content = content.Replace(@"\n", '\n'.ToString());
            }
            else
            {
                content = valueContext.expression_numeraire().VALEUR_NOMBRE().ToString();
            }
            
            executionConsole.Write(content);

            
            return CosmosData.WithKeyValue(CosmosData.Type.CsharpCode,$"Console.Write(\"{content}\")");
        }

        protected override CosmosData AggregateResult(CosmosData aggregate, CosmosData nextResult)
        {
            if (aggregate == null)
            {
                if (nextResult == null)
                {
                    return new CosmosData();
                }
                else
                {
                    return nextResult;
                }
            }
            else
            {
                return aggregate.Merge(nextResult);
            }
            
        }
    }
}