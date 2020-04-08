using System;
using Antlr4.Runtime.Tree;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<string>
    {
        //TODO : console wrapper pour les tests et plus si affinité...
        
        private const char StringDelimiter = '\"';

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
            
            Console.Write(content);

            return $"Console.Write(\"{content}\")";
        }
    }
}