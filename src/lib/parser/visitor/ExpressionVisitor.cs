using System;
using lib.antlr;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class ExpressionVisitor : CosmosBaseVisitor<CosmosTypedValue>
    {
        private readonly BooleanExpressionVisitor booleanExpressionVisitor;
        private readonly NumericExpressionVisitor numericExpressionVisitor;
        private readonly StringExpressionVisitor stringExpressionVisitor;
        private readonly VariableVisitor variableVisitor;

        public ExpressionVisitor(VariableVisitor variableVisitor,Parser parser, Random random)
        {
            this.variableVisitor = variableVisitor;

            stringExpressionVisitor = new StringExpressionVisitor(parser,variableVisitor);
            numericExpressionVisitor = new NumericExpressionVisitor(variableVisitor,random);
            booleanExpressionVisitor = new BooleanExpressionVisitor(this);
        }

        public VariableVisitor VariableVisitor => variableVisitor;

        public override CosmosTypedValue VisitExpression_booleenne(CosmosParser.Expression_booleenneContext context)
        {
            return booleanExpressionVisitor.Visit(context);
        }

        public override CosmosTypedValue VisitExpression_textuelle(CosmosParser.Expression_textuelleContext context)
        {
            return stringExpressionVisitor.Visit(context);
        }

        public override CosmosTypedValue VisitExpression_numerique(CosmosParser.Expression_numeriqueContext context)
        {
            return numericExpressionVisitor.Visit(context);
        }

        public override CosmosTypedValue VisitNombre(CosmosParser.NombreContext context)
        {
            return numericExpressionVisitor.VisitNombre(context);
        }

        public override CosmosTypedValue VisitVariable(CosmosParser.VariableContext context)
        {
            return variableVisitor.VisitVariable(context).Value;
        }

    }
}