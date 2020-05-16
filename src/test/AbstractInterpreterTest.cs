using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using lib.console;
using lib.interpreter;
using lib.parser;
using lib.parser.type;
using Xunit.Abstractions;

namespace test
{
    public abstract class AbstractInterpreterTest
    {
        protected const string DataFilePath = "../../../data/";

        private const string ValidHeaderSnippet = "Auteur: Jonathan Strokee\n" +
                                                  "Date: 27.03.2020\n" +
                                                  "Entreprise: ETML\n" +
                                                  "Description: Demonstration du langage cosmos,\n" +
                                                  "ce langage esttodo extraordinaire";

        private const string ValidStartSnippet =
            "Voici les ordres du programme DEMO_COSMOS à classer dans la bibliothèque DEMONSTRATION :";

        private const string ValidEnd = "Fin de la transmission.";

        protected const string TrueCondition = "1 vaut 1";
        protected const string FalseCondition = "2 vaut 3";

        protected const string Allocation = "\tAllouer une zone mémoire ";
        protected const string Afficher = "\tAfficher"; //no integrated tab as used as
        protected const string Repeter = "\tRépéter ";


        private readonly ITestOutputHelper helper;

        protected Parser parser;
        protected Interpreter interpreter;
        protected XUnitCompatibleConsole testConsole;

        protected Random random = new Random(27031983);

        public AbstractInterpreterTest(ITestOutputHelper helper)
        {
            testConsole = new XUnitCompatibleConsole(helper);
            this.helper = helper;
        }

        ~AbstractInterpreterTest()
        {
            //after hook
        }


        protected virtual void BuildFileInterpreter(string file)
        {
            parser = new Parser().ForFile(file).WithConsole(testConsole);
            interpreter = new Interpreter(parser,testConsole);
        }

        protected virtual void BuildSnippetInterpreter(string content, bool expectedParseResult = true)
        {
            var program = $"{ValidHeaderSnippet}\n{ValidStartSnippet}\n{content}\n{ValidEnd}";

            var programWithLines = new StringBuilder();
            var i = 1;
            foreach (var line in program.Split("\n"))
            {
                programWithLines.Append(i++).Append(" ").Append(line).Append("\n");
            }

            helper.WriteLine($"///Code source----\n{programWithLines}\n///Fin du code source----\n\nRésultat d'éxécution:\n",IConsole.Channel.Debug);

            parser = new Parser().ForSnippet(program).WithConsole(testConsole);
            interpreter = new Interpreter(parser,testConsole).WithRandom(random);

            using (var scope = new AssertionScope())
            {
                parser.Parse().Should().Be(expectedParseResult);
                if (expectedParseResult)
                {
                    parser.ErrorListener.Errors.Should().BeEmpty();
                }
                else
                {
                    parser.ErrorListener.Errors.Should().HaveCountGreaterThan(0);
                }

            }
        }

        protected string BuildIfStatement(bool condition, List<bool> elsifs = null, bool? elsee = null)
        {
            var result = new StringBuilder();
            const string function = Afficher;
            result.Append(
                $"\tSi {(condition ? TrueCondition : FalseCondition)} alors\n\t{function} \"{(condition ? TrueCondition : FalseCondition)}\".\n\t");
            if (elsifs != null)
                foreach (var elsif in elsifs)
                    result.Append(
                        $"sinon si {(elsif ? TrueCondition : FalseCondition)} alors\n\t{function} \"{(elsif ? TrueCondition : FalseCondition)}\".\n\t");

            if (elsee != null) result.Append($"et sinon\n\t{function} \"{TrueCondition}\".\n\t");

            result.Append("?\n");

            var resultString = result.ToString();

            //helper.WriteLine(resultString);

            return resultString;
        }

        protected string BuildAllocationSnippet(string variableName, string variableExpression,
            string variablePrefix = "")
        {
            var result =
                $"{Allocation} {variableName} {(variableExpression != null ? $"avec {variablePrefix} {variableExpression}" : "")}.\n";
            return result;
        }

        protected string BuildAllocationSnippet(CosmosVariable variable)
        {
            string value = null;
            if (variable.Value != null) value = variable.Value.ToString();

            return BuildAllocationSnippet(variable.Name, value);
        }

        protected string BuildAfficherSnippet(string content)
        {
            var result = new StringBuilder();
            var finalContent = content;
            if (!content.StartsWith("\""))
            {
                finalContent = $"\"{content}\"";
            }
            result.Append($"{Afficher} {finalContent}.\n");

            return result.ToString();
        }

        protected string BuildStaticLoopSnippet(int iterations,string content)
        {
            var result = new StringBuilder();
            result.Append($"{Repeter} {iterations}x\n");
            result.Append($"\t"+content);
            result.Append($"\n\t>>");

            return result.ToString();
        }

        protected string BuildDynamicLoopSnippet(CosmosVariable iterations,string content, int version=0)
        {
            var result = new StringBuilder();
            result.Append(BuildAllocationSnippet(iterations));
            result.Append(Repeter);
            if (version == 0)
            {
                result.Append($"le nombre de fois correspondant à la valeur enregistrée dans la zone mémoire {iterations.Name}\n");
            }
            else if (version == 1)
            {
                result.Append($"autant de fois qu'il y a de {iterations.Name}\n");
            }

            result.Append("\t"+content);
            result.Append($"\n\t>>\n");

            return result.ToString();
        }

        protected string BuildWhileLoopSnippet(string boolExpression,string content)
        {
            var result = new StringBuilder();
            result.Append($"{Repeter} tant que {boolExpression} \n");
            result.Append($"\t"+content);
            result.Append($"\n\t>>\n");

            return result.ToString();
        }
    }
}