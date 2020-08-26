using Antlr4.Runtime;

namespace lib.parser.exception
{
    public class UnknownVariableException : CosmosException
    {
        public UnknownVariableException(string name,ParserRuleContext context) :
            base($"{BuildParseErrorHeader(context)} Espace memoire/Variable {name} inconnue " +
                 $"(l'avez-vous declaree ou y'a-t-il une erreur d'orthographe ?)")
        {
        }
    }
}