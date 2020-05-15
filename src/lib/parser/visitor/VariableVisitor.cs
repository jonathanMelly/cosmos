using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using lib.antlr;
using lib.parser.exception;
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
            return GetVariable(context.la_zone_memoire().VARIABLE(), context);
        }

        public CosmosVariable GetVariable(ITerminalNode variableNode, ParserRuleContext context)
        {
            var variableName = variableNode.GetText();

            if (parser.Variables.ContainsKey(variableName)) return parser.Variables[variableName];

            throw new UnknownVariableException(variableName,context);
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