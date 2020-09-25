using System.Text.RegularExpressions;
using lib.antlr;
using lib.extension;
using lib.parser.exception;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class StringExpressionVisitor : CosmosBaseVisitor<CosmosString>
    {
        private readonly Parser parser;
        private readonly VariableVisitor variableVisitor;

        //For simple cases, # prefix is enough... When not, one can use {} delimiters...
        private readonly Regex variableRegex = new Regex(@"{?(##?\w(\.?\w)*)}?");

        public StringExpressionVisitor(Parser parser,VariableVisitor variableVisitor)
        {
            this.parser = parser;
            this.variableVisitor = variableVisitor;
        }

        public override CosmosString VisitExpression_textuelle(Cosmos.Expression_textuelleContext context)
        {
            switch (context.GetChild(0))
            {
                case Cosmos.Atome_textuelContext atomeTextuelContext:
                    var valueExpression = atomeTextuelContext.chaine_de_caractere().VALEUR_TEXTE().GetText();

                    //For each match, it returns the stored value (if found)
                    valueExpression = variableRegex.Replace(valueExpression, match =>
                    {
                        var varName = match.Value;

                        //Handles special delimiters...
                        if (varName.StartsWith("{") && varName.EndsWith("}"))
                        {
                            varName = varName.Substring(1, varName.Length - 2);
                        }

                        try
                        {
                            var storedVariable = variableVisitor.GetVariable(varName, atomeTextuelContext);
                            return storedVariable.Value==null ?"<NÉANT>":storedVariable.Value.ToString();
                        }
                        catch (UnknownVariableException e)
                        {
                            parser.ErrorListener.Error(e);
                            return match.Value; //return refName as value
                        }


                    });

                    return valueExpression.

                        //Removes surrounding "
                        Substring(1, valueExpression.Length - 2).
                        //Fix special char
                        Replace(@"\n", '\n'.ToString()).AsCosmosString();
            }

            throw new MissingTokenHandlerException(context);
        }
    }
}