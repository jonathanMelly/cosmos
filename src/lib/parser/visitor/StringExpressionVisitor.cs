using System.Text.RegularExpressions;
using lib.antlr;
using lib.extension;
using lib.parser.exception;
using lib.parser.type;

namespace lib.parser.visitor
{
    public class StringExpressionVisitor : CosmosBaseVisitor<CosmosString>
    {
        private readonly Parser parser;
        private readonly Regex variableRegex = new Regex(@"#\w+");

        public StringExpressionVisitor(Parser parser)
        {
            this.parser = parser;
        }

        public override CosmosString VisitExpression_textuelle(CosmosParser.Expression_textuelleContext context)
        {
            switch (context.GetChild(0))
            {
                case CosmosParser.Atome_textuelContext atomeTextuelContext:
                    var valueExpression = atomeTextuelContext.chaine_de_caractere().VALEUR_TEXTE().GetText();

                    valueExpression = variableRegex.Replace(valueExpression, match =>
                    {
                        if (parser.Variables.ContainsKey(match.Value))
                        {
                            return parser.Variables[match.Value].Value.ToString();
                        }

                        parser.ErrorListener.Error(new UnknownVariableException(match.Value,atomeTextuelContext.chaine_de_caractere()));
                        return match.Value;
                    });

                    return valueExpression.

                        //Removes surrounding "
                        Substring(1, valueExpression.Length - 2).
                        //Fix special char
                        Replace(@"\n", '\n'.ToString()).AsCosmosString();
            }

            throw new MissingTokenHandlerException(context);
        }
    }
}