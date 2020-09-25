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

        public override CosmosNumber VisitVariable(Cosmos.VariableContext context)
        {
            return variableVisitor.Visit(context).Value.Number();
        }

        public override CosmosNumber VisitExpression_numerique(Cosmos.Expression_numeriqueContext context)
        {
            switch (context.GetChild(0))
            {
                case Cosmos.Expression_numeriqueContext _:
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

                case Cosmos.Atome_numeriqueContext atomeNumeriqueContext:
                    return Visit(atomeNumeriqueContext.nombre());

                case Cosmos.NombreContext nombreContext:
                    return VisitNombre(nombreContext);

                case Cosmos.VariableContext variableContext:
                    var variable = variableVisitor.Visit(variableContext);
                    if (variable.Value == null)
                    {
                        throw new EmptyVariableException(variable,variableContext);
                    }
                    else
                    {
                        return variable.Value.Number();
                    }


                default:
                    if (context.PARENTHESE_GAUCHE() != null)
                        return Visit(context.sousExpression);
                    else
                        return context.operateur.Type switch
                        {
                            OPERATEUR_MATH_MOINS => -Visit(context.sousExpression),
                            OPERATEUR_MATH_PLUS  => Visit(context.sousExpression),

                            _ => throw new MissingTokenHandlerException(context.operateur)
                        };
            }


        }

        public override CosmosNumber VisitNombre(Cosmos.NombreContext context)
        {
            return new CosmosNumber(context.VALEUR_NOMBRE().GetText());
        }
    }
}