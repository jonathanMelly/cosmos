using lib.antlr;
using lib.extension;
using lib.parser.exception;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class StringExpressionVisitor : CosmosBaseVisitor<CosmosString>
    {

        public override CosmosString VisitExpression_textuelle(CosmosParser.Expression_textuelleContext context)
        {
            switch (context.GetChild(0))
            {
                case CosmosParser.Atome_textuelContext atomeTextuelContext:
                    var valueExpression = atomeTextuelContext.chaine_de_caractere().VALEUR_TEXTE().GetText();

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