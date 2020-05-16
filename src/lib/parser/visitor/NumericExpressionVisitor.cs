using System;
using lib.antlr;
using lib.parser.exception;
using lib.parser.type;
using static lib.antlr.CosmosLexer;

namespace lib.parser.visitor
{
    public class NumericExpressionVisitor : CosmosBaseVisitor<CosmosNumber>
    {
        private readonly VariableVisitor variableVisitor;
        private readonly Random random;

        public NumericExpressionVisitor(VariableVisitor variableVisitor,Random random)
        {
            this.variableVisitor = variableVisitor;
            this.random = random;
        }

        public override CosmosNumber VisitVariable(CosmosParser.VariableContext context)
        {
            return variableVisitor.Visit(context).Value.Number();
        }

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
                    if (atomeNumeriqueContext.nombre_aleatoire() != null)
                    {
                        var min = Convert.ToInt32(atomeNumeriqueContext.nombre_aleatoire().min.GetText());
                        var max = Convert.ToInt32(atomeNumeriqueContext.nombre_aleatoire().max.GetText());
                        return new CosmosNumber(random.Next(min,max+1));
                    }
                    return new CosmosNumber(atomeNumeriqueContext.nombre().VALEUR_NOMBRE().GetText());

                case CosmosParser.VariableContext variableContext:
                    return variableVisitor.Visit(variableContext).Value.Number();

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