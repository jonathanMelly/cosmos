using lib.antlr;

namespace lib
{
    public class VariableVisitor : CosmosBaseVisitor<CosmosVariable>
    {
        private readonly Interpreter interpreter;

        private ExpressionVisitor expressionVisitor;

        public VariableVisitor(Interpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void SetExpressionVisitor(ExpressionVisitor expressionVisitor)
        {
            this.expressionVisitor = expressionVisitor;
        }

        public override CosmosVariable VisitVariable(CosmosParser.VariableContext context)
        {
            var variableName = context.VARIABLE().GetText();

            if (interpreter.Variables.ContainsKey(variableName)) return interpreter.Variables[variableName];

            interpreter.ErrorListener.UnknownVariableError(context.start.Line, context.start.Column, variableName);
            //throw new MissingTokenHandlerException(context,firstChild.GetText());

            return null;
        }

        public override CosmosVariable VisitAllouer(CosmosParser.AllouerContext context)
        {
            return new CosmosVariable
            (
                context.zone_memoire().VARIABLE().GetText(),
                context.expression() != null
                    ? expressionVisitor.Visit(context.expression())
                    : null
            );
        }
    }
}