using lib.antlr;
using lib.extension;
using lib.parser.exception;
using lib.parser.type;
using static lib.antlr.CosmosLexer;

namespace lib.parser.visitor
{
    public class BooleanExpressionVisitor : CosmosBaseVisitor<CosmosBoolean>
    {
        private readonly ExpressionVisitor expressionVisitor;

        public BooleanExpressionVisitor(ExpressionVisitor expressionVisitor)
        {
            this.expressionVisitor = expressionVisitor;
        }

        public override CosmosBoolean VisitVariable(Cosmos.VariableContext context)
        {
            return expressionVisitor.VariableVisitor.Visit(context).Value.Boolean();
        }

        public override CosmosBoolean VisitExpression_booleenne(Cosmos.Expression_booleenneContext context)
        {
            var firstChild = context.GetChild(0);
            switch (firstChild)
            {
                case Cosmos.Expression_booleenneContext _:
                    var left = Visit(context.gauche).Boolean().Value;
                    var right = context.droite;
                    var booleanOperator = context.operateur;

                    bool resultb;
                    switch (booleanOperator.Type)
                    {
                        case OPERATEUR_LOGIQUE_OU:
                            resultb = left || Visit(right).Boolean().Value;
                            break;
                        case OPERATEUR_LOGIQUE_ET:
                        case ET:
                            resultb = left && Visit(right).Boolean().Value;
                            break;
                        case OPERATEUR_LOGIQUE_EST:
                            var rightValue = context.VRAI() != null;
                            resultb = left == rightValue;
                            break;
                        case OPERATEUR_LOGIQUE_OU_EXCLUSIF:
                            resultb = left ^ Visit(right).Boolean().Value;
                            break;
                        case OPERATEUR_COMPARAISON_EQUIVALENT:
                            resultb = left == Visit(right).Boolean().Value;
                            break;
                        case OPERATEUR_COMPARAISON_DIFFERENT:
                            resultb = left != Visit(right).Boolean().Value;
                            break;
                        default:
                            throw new MissingTokenHandlerException(booleanOperator);
                    }

                    return resultb.AsCosmosBoolean();

                case Cosmos.Expression_comparableContext _:
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

                case Cosmos.VariableContext variableContext:
                    return expressionVisitor.VariableVisitor.Visit(variableContext).Value.Boolean();


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