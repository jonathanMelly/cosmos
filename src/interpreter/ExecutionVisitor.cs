using System;
using System.Text;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<ExecutionContext>
    {
        private IConsole executionConsole;
        private const char StringDelimiter = '\"';

        private readonly Interpreter interpreter;
        
        
        //Used to sanitize value expressions that can be prefixed by some semantic text...
        private readonly Regex noSemanticPrefixRegex = new Regex(@"^[^#\d\(]*(.*)$");
        
        //As dynamicExpresso is used, i parse again the code looking for variables instead of using the visitor...
        private readonly Regex varRegex = new Regex("#\\w+");

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

        public override ExecutionContext VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            this.executionConsole = executionConsole ?? new DefaultConsole();
            return base.VisitProgramme(context);
        }

        public override ExecutionContext VisitSelection(CosmosParser.SelectionContext context)
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
            var left = ConvertToCsharpType(context.left);
            var right = ConvertToCsharpType(context.right);

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
        

        private object ConvertToCsharpType(CosmosParser.ExpressionContext context)
        {
            
            var computableContext= context.expression_calculable();
            if (computableContext != null)
            {
                string expressionText = computableContext.GetText();
                string cleanedExpression = noSemanticPrefixRegex.Replace(expressionText, "$1");
                
                var expressionInterpreter = new DynamicExpresso.Interpreter();
                var expressoExpression = new StringBuilder(cleanedExpression);
                
                //TODO try catch with mixing var types for example !! + test ?
                foreach (Match match in varRegex.Matches(cleanedExpression))
                {
                    string variableName = match.Value;
                    string cleanedName = variableName.Substring(1);
                    //removes # (incompatible with expresso)
                    expressoExpression.Replace(variableName, cleanedName);
                    if (interpreter.Variables.ContainsKey(variableName))
                    {
                        expressionInterpreter.SetVariable(cleanedName,interpreter.Variables[variableName].Value);
                    }
                    else
                    {
                        interpreter.ErrorListener.UnknownVariableError(context.start.Line,context.start.Column,variableName);
                    }

                }

                return expressionInterpreter.Eval<Decimal>(expressoExpression.ToString());

            }
            else if (context.expression_textuelle() != null)
            {
                //TODO regex ?
                return context.expression_textuelle().VALEUR_TEXTE().ToString().
                    TrimStart(StringDelimiter).TrimEnd(StringDelimiter).
                    Replace(@"\n", '\n'.ToString());
            }
            

            return null;
        }

        public override ExecutionContext VisitAfficher(CosmosParser.AfficherContext context)
        {
            executionConsole.Write(ConvertToCsharpType(context.expression()).ToString());

            return null;
        }

        public override ExecutionContext VisitAllouer(CosmosParser.AllouerContext context)
        {
            Variable variable = ConvertToVariable(context);
            interpreter.Variables[variable.Name] = variable;            

            return null;
        }

        Variable ConvertToVariable(CosmosParser.AllouerContext context)
        {
            var variable = new Variable(context.zone_memoire().VARIABLE().GetText());

            if (context.expression() != null)
            {
                variable.Value = ConvertToCsharpType(context.expression());
            }
            
            return variable;
        }
    }
}