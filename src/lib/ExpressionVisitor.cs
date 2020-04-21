using lib.antlr;

namespace lib
{
    public class ExpressionVisitor : CosmosBaseVisitor<CosmosTypedValue>
    {
        private readonly BooleanExpressionVisitor booleanExpressionVisitor;
        private readonly NumericExpressionVisitor numericExpressionVisitor;
        private readonly StringExpressionVisitor stringExpressionVisitor;
        private readonly VariableVisitor variableVisitor;

        public ExpressionVisitor(Interpreter interpreter, VariableVisitor variableVisitor)
        {
            this.variableVisitor = variableVisitor;

            stringExpressionVisitor = new StringExpressionVisitor();
            numericExpressionVisitor = new NumericExpressionVisitor();
            booleanExpressionVisitor = new BooleanExpressionVisitor(this);
        }

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

        public override CosmosTypedValue VisitVariable(CosmosParser.VariableContext context)
        {
            return variableVisitor.VisitVariable(context).Value;
        }

        /*
        public override CosmosTypedValue VisitExpression_non_booleenne(CosmosParser.Expression_non_booleenneContext context)
        {
            return context.GetChild(0) switch
            {
                CosmosParser.Expression_textuelleContext textuelleContext => Visit(textuelleContext),
                CosmosParser.Expression_numeriqueContext numeriqueContext => Visit(numeriqueContext),
                
                _ => throw new MissingTokenHandlerException(context)
            };
        }


        /// <summary>
        /// Main dispatcher
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="MissingTokenHandlerException"></exception>
        public override CosmosTypedValue VisitExpression(CosmosParser.ExpressionContext context)
        {
            return context.GetChild(0) switch
            {
                CosmosParser.VariableContext  variableContext => variableVisitor.Visit(variableContext).Value,
                CosmosParser.Expression_booleenneContext booleenneContext => Visit(booleenneContext),
                CosmosParser.Expression_non_booleenneContext nonBooleenneContext => Visit(nonBooleenneContext),
                
                _ => throw new MissingTokenHandlerException(context)
            };
        }
        */
    }
}