using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<object>
    {
        private IConsole executionConsole;
        private const char StringDelimiter = '\"';

        private Interpreter interpreter;

        public ExecutorVisitor(Interpreter interpreter)
        {
            this.interpreter = interpreter;
        }
        
        /// <summary>
        /// Set customized output
        /// </summary>
        /// <param name="console"></param>
        public ExecutorVisitor WithConsole(IConsole console)
        {
            this.executionConsole = console;
            return this;
        }

        public override object VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            this.executionConsole = executionConsole ?? new DefaultConsole();
            return base.VisitProgramme(context);
        }

        public override object VisitSelection(CosmosParser.SelectionContext context)
        {
            
            var evaluation = EvaluateCondition(context.si);
            if (evaluation)
            {
                foreach (var instructionIntegree in context.instruction())
                {
                    Visit(instructionIntegree);
                }
            }
            else if (context.sinon_si() != null)
            {
                foreach (var elsif in context.sinon_si())
                {
                    if(EvaluateCondition(elsif.condition()))
                    {
                        foreach (var instructionIntegree in elsif.instruction())
                        {
                            Visit(instructionIntegree);
                        }
                        return null;//only 1 elsif branch
                    }
                }
            }
            
            if (context.sinon() != null)
            {
                foreach (var instructionIntegree in context.sinon().instruction())
                {
                    Visit(instructionIntegree);
                }
            }

            return null;
        }

        private bool EvaluateCondition(CosmosParser.ConditionContext context)
        {
            var left = Encode(context.left);
            var right = Encode(context.right);

            var test = GetLexerType(context.operateur_comparaison()) switch
            {
                CosmosLexer.OPERATEUR_COMPARAISON_EGAL => left.Equals(right),
                CosmosLexer.OPERATEUR_DIFFERENT => !left.Equals(right),
                _ => false
            };
            
            //Multiple conditions
            if (context.postcondition() != null)
            {
                foreach (var condition in context.postcondition())
                {
                    switch (GetLexerType(condition.operateur_booleen()))
                    {
                        case CosmosLexer.ET:
                            if (test)
                            {
                                test = EvaluateCondition(condition.condition());
                            }
                            else
                            {
                                //Short-circuit and
                                return false;
                            }
                            
                            break;
                        case  CosmosLexer.OU:
                            if (test)
                            {
                                //short-circuit true
                                return true;
                            }
                            else
                            {
                                test = EvaluateCondition(condition.condition());
                            }
                            break;
                        case CosmosLexer.OU_EXCLUSIF:
                            test ^= EvaluateCondition(condition.condition());
                            break;
                    }
                }
                
            }

            return test;
        }

        private int GetLexerType(ParserRuleContext parserRuleContext)
        {
            if (parserRuleContext == null) throw new ArgumentNullException(nameof(parserRuleContext));
            return ((ITerminalNode) parserRuleContext.GetChild(0)).Symbol.Type;
        }

        private object Encode(CosmosParser.Expression_valeurContext context)
        {
            CosmosParser.Expression_numeraireContext numericContext;
            if ((numericContext = context.expression_numeraire()) != null)
            {
                //TODO : types plus fins ?
                return Convert.ToDecimal(numericContext.VALEUR_NOMBRE().ToString());
            }
            else if (context.expression_textuelle() != null)
            {
                return context.expression_textuelle().VALEUR_TEXTE().ToString();
            }
            //TODO variable non définie
            else if (context.expression_variable() != null)
            {
                return interpreter.Variables[context.expression_variable().VARIABLE().GetText()];
            }
            //TODO else

            return context.expression_textuelle().VALEUR_TEXTE().ToString();
        }

        public override object VisitAfficher(CosmosParser.AfficherContext context)
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


            return null;
        }

        public override object VisitAllouer(CosmosParser.AllouerContext context)
        {
            Variable variable = Encode(context);
            interpreter.Variables[variable.Name] = variable;            

            return null;
        }

        Variable Encode(CosmosParser.AllouerContext context)
        {
            var variable = new Variable(context.zone_memoire().VARIABLE().GetText());

            if (context.expression_valeur() != null)
            {
                variable.Value = Encode(context.expression_valeur());
            }
            
            return variable;
        }
    }
}