using lib.antlr;
using lib.parser.type;

namespace lib.parser.exception
{
    public class EmptyVariableException : CosmosException
    {

        public EmptyVariableException(CosmosVariable variable, Cosmos.VariableContext variableContext) :
            base(variableContext,$"La variable {variable.Name} n'a pas de valeur définie")
        {
        }
    }
}