using lib.antlr;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class VariableVisitor : CosmosBaseVisitor<CosmosVariable>
    {
        private readonly Parser parser;

        private ExpressionVisitor expressionVisitor;

        public VariableVisitor(Parser parser)
        {
            this.parser = parser;
        }

        public ExpressionVisitor ExpressionVisitor
        {
            get => expressionVisitor;
            set => expressionVisitor = value;
        }

        public override CosmosVariable VisitVariable(CosmosParser.VariableContext context)
        {
            var variableName = context.la_zone_memoire().VARIABLE().GetText();

            if (parser.Variables.ContainsKey(variableName)) return parser.Variables[variableName];

            parser.ErrorListener.UnknownVariableError(context.start.Line, context.start.Column, variableName);
            //throw new MissingTokenHandlerException(context,firstChild.GetText());

            return null;
        }

        public override CosmosVariable VisitAllouer(CosmosParser.AllouerContext context)
        {
            return new CosmosVariable
            (
                context.une_zone_memoire().VARIABLE().GetText(),
                context.expression() != null
                    ? ExpressionVisitor.Visit(context.expression())
                    : null
            );
        }
    }
}