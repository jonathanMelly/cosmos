using interpreter.antlr;
using static interpreter.antlr.CosmosLexer;

namespace interpreter
{
    public class NumericExpressionVisitor : CosmosBaseVisitor<CosmosNumber>
    {

        public override CosmosNumber VisitExpression_numerique(CosmosParser.Expression_numeriqueContext context)
        {
            switch (context.GetChild(0))
            {
                case CosmosParser.Expression_numeriqueContext _:
                    var left = Visit(context.gauche);
                    var right = Visit(context.droite);
                    return context.operateur.Type switch
                    {
                        OPERATEUR_MATH_PUISSANCE => left ^ right,
                        OPERATEUR_MATH_RACINE_CARREE => left.Nroot(right),
                        OPERATEUR_MATH_FOIS => left * right,
                        OPERATEUR_MATH_DIVISE => left / right,
                        OPERATEUR_MATH_PLUS => left + right,
                        OPERATEUR_MATH_MOINS => left - right,

                        _ => throw new MissingTokenHandlerException(context.operateur)
                    };

                case CosmosParser.Atome_numeriqueContext atomeNumeriqueContext:
                    return new CosmosNumber(atomeNumeriqueContext.nombre().VALEUR_NOMBRE().GetText());

                default:
                    if (context.PARENTHESE_GAUCHE() != null)
                        return Visit(context.sousExpression);
                    else
                        return context.operateur.Type switch
                        {
                            OPERATEUR_MATH_MOINS => -Visit(context.sousExpression),
                            OPERATEUR_MATH_PLUS | PARENTHESE_GAUCHE => Visit(context.sousExpression),

                            _ => throw new MissingTokenHandlerException(context.operateur)
                        };
            }
        }
    }
}