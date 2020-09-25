using Antlr4.Runtime;

namespace lib.parser.exception
{
    public class UnknownVariableException : CosmosException
    {
        public UnknownVariableException(string name,ParserRuleContext context) :
            base(context,$" Espace mémoire (variable) {name} inconnu " +
                 $"(l'avez-vous bien créé ou y'a-t-il une erreur d'orthographe ?)")
        {
        }
    }
}