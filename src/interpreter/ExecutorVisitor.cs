using System;
using System.Diagnostics;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<string>
    {
        private Console executionConsole;
        private const char StringDelimiter = '\"';
        
        private readonly StringBuilder csharpSource = new StringBuilder();
        
        /// <summary>
        /// Set customized output
        /// </summary>
        /// <param name="console"></param>
        public ExecutorVisitor WithConsole(Console console)
        {
            this.executionConsole = console;
            return this;
        }

        public override string VisitProgramme(CosmosParser.ProgrammeContext context)
        {
            this.executionConsole = executionConsole ?? new DefaultConsole();
            return base.VisitProgramme(context);
        }

        public override string VisitSelection(CosmosParser.SelectionContext context)
        {
            //TODO : ajouter le code même si condition fausse ?
            var snippet = new StringBuilder(); 
                
            var result = ComputeCondition(context.condition());
            if (result.Item2)
            {
                foreach (var instructionIntegree in context.instruction_integree())
                {
                    snippet.Append(Visit(instructionIntegree));
                }
                
                snippet.Append(result.Item1);
            }

            
            return snippet.ToString();
        }

        public Tuple<string,bool> ComputeCondition(CosmosParser.ConditionContext context)
        {
            bool test=false;
            object left = ComputeValue(context.left);
            object right = ComputeValue(context.right);

            //https://stackoverflow.com/questions/33198762/how-can-i-determine-which-alternative-node-was-chosen-in-antlr/33207750 ??
            ITerminalNode operatorType = context.operateur_comparaison().GetChild(0) as ITerminalNode;
            Debug.Assert(operatorType != null, nameof(operatorType) + " != null");
            switch (operatorType.Symbol.Type)
            {
                case CosmosLexer.OPERATEUR_EGAL:
                    test = left.Equals(right);
                    break;
                case CosmosLexer.OPERATEUR_DIFFERENT:
                    test = !left.Equals(right);
                    break;
            }

            return Tuple.Create(left.ToString()+context.operateur_comparaison().ToString()+right.ToString(),test);
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

        /// <summary>
        /// Executes the function
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string VisitInstruction_simple_base(CosmosParser.Instruction_simple_baseContext context)
        {
            string result=null;
            ParserRuleContext functionContext;
            if ((functionContext = context.afficher()) != null)
            {
                result = VisitAfficher((CosmosParser.AfficherContext) functionContext);
                csharpSource.Append(result);

            }

            return result;
        }

        public override string VisitAfficher(CosmosParser.AfficherContext context)
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

            return $"Console.Write(\"{content}\")";
        }
    }
}