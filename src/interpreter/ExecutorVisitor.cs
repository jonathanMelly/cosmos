using System;
using interpreter.antlr;

namespace interpreter
{
    public class ExecutorVisitor : CosmosBaseVisitor<string>
    {
        private const char StringDelimiter = '\"';

        public override string VisitAfficher(CosmosParser.AfficherContext context)
        {
            string content;
            if (context.VALEUR_NOMBRE() == null)
            {
                content = context.VALEUR_TEXTE().ToString().TrimStart(StringDelimiter).TrimEnd(StringDelimiter);
            }
            else
            {
                content = context.VALEUR_NOMBRE().ToString();
            }
            
            Console.Write(content);

            return "Console.Write(\"content\")";
        }
    }
}