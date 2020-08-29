using System.Collections.Generic;
using Antlr4.Runtime;
using lib.antlr;
using lib.console;
using lib.extension;
using lib.parser.exception;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class VariableVisitor : CosmosBaseVisitor<CosmosVariable>
    {

        private readonly Parser parser;

        private ExpressionVisitor expressionVisitor;

        private const string DATE_VAR_PREFIX = "##date.";
        private const string DATE_VAR_DAY = DATE_VAR_PREFIX + "jour";
        private const string DATE_VAR_MONTH = DATE_VAR_PREFIX + "mois";
        private const string DATE_VAR_YEAR = DATE_VAR_PREFIX + "année";
        private const string DATE_VAR_HOUR = DATE_VAR_PREFIX + "heure";
        private const string DATE_VAR_MINUTE = DATE_VAR_PREFIX + "minute";
        private const string DATE_VAR_SECOND = DATE_VAR_PREFIX + "seconde";

        private const string KEY_VAR_PREFIX = "##touche";
        private readonly string KEY_VAR_AVAILABLE = $"{KEY_VAR_PREFIX}.disponible";
        private readonly string KEY_VAR_KEY = $"{KEY_VAR_PREFIX}";

        private readonly string[] DATE_VARS = new[]{DATE_VAR_DAY,
            DATE_VAR_MONTH,
            DATE_VAR_YEAR,

            DATE_VAR_HOUR,
            DATE_VAR_MINUTE,
            DATE_VAR_SECOND};

        private IConsole console;

        public VariableVisitor(Parser parser,IConsole console)
        {
            this.parser = parser;
            this.console = console;
        }

        public ExpressionVisitor ExpressionVisitor
        {
            get => expressionVisitor;
            set => expressionVisitor = value;
        }

        public override CosmosVariable VisitVariable(CosmosParser.VariableContext context)
        {
            return VisitLa_zone_memoire(context.la_zone_memoire());
        }

        public override CosmosVariable VisitLa_zone_memoire(CosmosParser.La_zone_memoireContext context)
        {
            return GetVariable(context.VARIABLE().GetText(), context);
        }

        public CosmosVariable GetVariable(string varName, ParserRuleContext context)
        {

            //Look for special vars
            //28.08.2020 : Choice is to fill the map because of optional future gui memory analysis tool...
            if (varName.StartsWith(DATE_VAR_PREFIX))
            {
                foreach (var dateVar in DATE_VARS)
                {
                    if (varName == dateVar)
                    {
                        RefreshDateVariable(varName,parser.Variables);
                        break;
                    }
                }
            }
            else if (varName == KEY_VAR_AVAILABLE)
            {
                Fill(parser.Variables,KEY_VAR_AVAILABLE,console.KeyAvailable.AsCosmosBoolean());
            }
            else if (varName == KEY_VAR_KEY)
            {
                var key = console.ReadKey;
                Fill(parser.Variables,KEY_VAR_KEY,key?.AsCosmosString());
            }

            if (parser.Variables.ContainsKey(varName))
            {
                return parser.Variables[varName];
            }

            throw new UnknownVariableException(varName,context);
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

        private void RefreshDateVariable(string variableName, IDictionary<string, CosmosVariable> variables)
        {
            var now = Clock.Now;
            switch (variableName)
            {
                case DATE_VAR_DAY :
                    Fill(variables,DATE_VAR_DAY,now.Day.AsCosmosNumber(true));
                    break;
                case DATE_VAR_MONTH:
                    Fill(variables,DATE_VAR_MONTH,now.Month.AsCosmosNumber(true));
                    break;
                case DATE_VAR_YEAR:
                    Fill(variables,DATE_VAR_YEAR,now.Year.AsCosmosNumber());
                    break;
                case DATE_VAR_HOUR :
                    Fill(variables,DATE_VAR_HOUR,now.Hour.AsCosmosNumber(true));
                    break;
                case DATE_VAR_MINUTE:
                    Fill(variables,DATE_VAR_MINUTE,now.Minute.AsCosmosNumber(true));
                    break;
                case DATE_VAR_SECOND:
                    Fill(variables,DATE_VAR_SECOND,now.Second.AsCosmosNumber(true));
                    break;
            }
        }

        private void Fill(IDictionary<string, CosmosVariable> variables, string varName, CosmosTypedValue value)
        {
            variables[varName] = varName.AsCosmosVariable(value);
        }
    }
}