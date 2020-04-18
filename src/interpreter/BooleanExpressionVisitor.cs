using interpreter.antlr;
using interpreter.extensions;
using static interpreter.antlr.CosmosLexer;

namespace interpreter
{
    public class BooleanExpressionVisitor : CosmosBaseVisitor<CosmosBoolean>
    {
        private readonly ExpressionVisitor expressionVisitor;

        public BooleanExpressionVisitor(ExpressionVisitor expressionVisitor)
        {
            this.expressionVisitor = expressionVisitor;
        }

        public override CosmosBoolean VisitExpression_booleenne(CosmosParser.Expression_booleenneContext context)
        {
            var firstChild = context.GetChild(0);
            switch (firstChild)
            {
                case CosmosParser.Expression_booleenneContext _:
                    var left = Visit(context.gauche).Boolean().Value;
                    var right = context.droite;

                    switch (context.operateur.Type)
                    {
                        case OPERATEUR_LOGIQUE_OU:
                            return (left || Visit(right).Boolean().Value).AsCosmosBoolean();

                        case OPERATEUR_LOGIQUE_ET:
                            return (left && Visit(right).Boolean().Value).AsCosmosBoolean();

                        case OPERATEUR_LOGIQUE_OU_EXCLUSIF:
                            return (left ^ Visit(right).Boolean().Value).AsCosmosBoolean();
                    }

                    break;

                case CosmosParser.Expression_non_booleenneContext _:
                    var leftNb = expressionVisitor.Visit(context.gaucheNb);
                    var rightNb = expressionVisitor.Visit(context.droiteNb);

                    var result = context.operateurNb.Type switch
                    {
                        OPERATEUR_COMPARAISON_EQUIVALENT => leftNb == rightNb,
                        OPERATEUR_COMPARAISON_DIFFERENT => leftNb != rightNb,
                        OPERATEUR_COMPARAISON_PLUS_GRAND => leftNb > rightNb,
                        OPERATEUR_COMPARAISON_PLUS_PETIT => leftNb < rightNb,
                        OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL => leftNb >= rightNb,
                        OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL => leftNb <= rightNb,

                        _ => throw new MissingTokenHandlerException(context.operateurNb)
                    };

                    return result.AsCosmosBoolean();


                default:
                    if (context.OPERATEUR_LOGIQUE_NON() != null)
                        return !Visit(context.sousExpression);
                    else if (context.VRAI() != null)
                        return true.AsCosmosBoolean();
                    else if (context.FAUX() != null)
                        return false.AsCosmosBoolean();
                    else if (context.PARENTHESE_GAUCHE() != null) return Visit(context.sousExpression);

                    break;
            }

            throw new MissingTokenHandlerException(context);
        }
    }
}