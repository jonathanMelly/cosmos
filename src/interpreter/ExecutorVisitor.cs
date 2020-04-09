using System;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<string>
    {
        private Console executionConsole;
        private const char StringDelimiter = '\"';
        
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