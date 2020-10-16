using System.Threading;
using FluentAssertions;
using lib.extension;
using lib.parser.type;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestVariable : AbstractInterpreterTest
    {
        public TestVariable(ITestOutputHelper helper) : base(helper)
        {
            //Todo : allocation
            //Todo : affectation
        }



        private void TestValidAllocationWithValue(string variableExpression, CosmosTypedValue expectedValue,
            string variablePrefix = "")
        {
            //Arrange
            var variableName = "#maVariable";
            var expectedResult = variableName.AsCosmosVariable(expectedValue);

            BuildSnippetInterpreter(BuildAllocationSnippet(variableName, variableExpression));

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables.Should().HaveCount(1).And.ContainKey(expectedResult.Name).WhichValue.Should()
                .BeEquivalentTo(expectedResult);
        }

        private void TestInValidAllocationWithValue(string variableExpression)
        {
            //Arrange
            var variableName = "#maVariable";

            BuildSnippetInterpreter(BuildAllocationSnippet(variableName, variableExpression), false);

            //Act-assert
            interpreter.Execute().Should().BeFalse();
        }



        [Fact]
        public void TestEmptyAllocationNumbered()
        {
            //Arrange
            var variable = "#0".AsCosmosVariable();
            BuildSnippetInterpreter(Allocation + variable.Name + ".");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables.Should().HaveCount(1).And.ContainKey(variable.Name).WhichValue.Value.Should()
                .BeNull();
        }

        [Fact]
        public void TestEmtpyAllocationNamed()
        {
            //Arrange
            var variable = "#maVariable".AsCosmosVariable();
            var snippet = BuildAllocationSnippet(variable);
            BuildSnippetInterpreter(snippet);

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables.Should().HaveCount(1).And.ContainKey(variable.Name).WhichValue.Value.Should()
                .BeNull();
        }

        [Fact]
        public void TestNumberAllocationVariant1()
        {
            var value = 55;
            TestValidAllocationWithValue(value.ToString(), value.AsCosmosNumber(), "le nombre");
        }

        [Fact]
        public void TestNumberAllocationVariant2()
        {
            TestValidAllocationWithValue("66.6", 66.6.AsCosmosNumber());
        }

        [Fact]
        public void TestNumberExpressionAllocation()
        {
            TestValidAllocationWithValue("1+2*3", 7.AsCosmosNumber());
        }

        [Fact]
        public void TestNumberWithParenthesesExpressionAllocation()
        {
            TestValidAllocationWithValue("(1+2)*3", 9.AsCosmosNumber());
        }

        [Fact]
        public void TestRefToVariableAllocation()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());

            var copy = "#copy".AsCosmosVariable(0.AsCosmosNumber());

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildAllocationSnippet(copy.Name, variableRef.Name));


            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables.Should().HaveCount(2).And.ContainKey(variableRef.Name).And.ContainKey(copy.Name)
                .WhichValue.Value.Should().NotBe(copy.Value).And.Be(variableRef.Value);
        }

        [Fact]
        public void TestRefToUnknownVariableAllocation()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());

            var copy = "#copy".AsCosmosVariable(0.AsCosmosNumber());

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildAllocationSnippet(copy.Name, variableRef.Name + $"Error"));


            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should().Contain("inconnu");
        }

        [Fact]
        public void TestStringAllocationVariant1BadPrefix()
        {
            var value = "StringAllocation1BadPrefix";
            TestInValidAllocationWithValue($"le (t)exte \"{value}\"");
        }

        [Fact]
        public void TestStringAllocationVariant1Ok()
        {
            var value = "StringAllocation1";
            TestValidAllocationWithValue($"\"{value}\"", value.AsCosmosString(), "le texte");
        }

        [Fact]
        public void TestStringAllocationVariant2()
        {
            var value = "StringAllocation2";
            TestValidAllocationWithValue($"\"{value}\"", value.AsCosmosString());
        }

        [Fact]
        public void TestCopyValue()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());
            var newVal = 5;

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildCopySnippet(newVal, variableRef.Name) +
                                    BuildAfficherSnippet(variableRef.Name.ToString()));


            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Be(newVal.ToString());
        }

        [Fact]
        public void TestCopyValueVariant2()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());
            var newVal = 5;

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildCopySnippet(newVal, variableRef.Name, 1) +
                                    BuildAfficherSnippet(variableRef.Name.ToString()));


            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Be(newVal.ToString());
        }

        [Fact]
        public void TestNoViableInputError()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());
            var newVal = "la valeur de (5+5)";

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildCopySnippet(newVal, $"{variableRef.Name}", 1) +
                                    BuildAfficherSnippet(variableRef.Name.ToString()), false);


            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should()
                .Be("Erreur, ligne 8:22 pas d'alternative viable à l'endroit ou il y a 'la valeur de ('\n");
        }

        [Fact]
        public void TestLeResultatDe()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());
            var newVal = "le résultat de (5+5)";

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildCopySnippet(newVal, $"{variableRef.Name}", 1) +
                                    BuildAfficherSnippet(variableRef.Name.ToString()));


            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Be("10");
        }

        [Fact]
        private void TestEmptyVariable()
        {
            //Arrange
            var variableName = "#maVariable";

            BuildSnippetInterpreter(BuildAllocationSnippet(variableName.AsCosmosVariable()) + "\n\t" +
                                    BuildAfficherSnippet(variableName));

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Be("<NÉANT>");
        }



        [Fact]
        private void TestBadAllocationWrongKeyword()
        {
            //Arrange

            BuildSnippetInterpreter("\tCréer la zone mémoire #bad.", false);

            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should()
                .Be("Erreur, ligne 7:7 élément invalide 'la ' attendu {'une ', VARIABLE}\n");
        }

        [Fact]
        private void TestBadAllocationBadCharacter()
        {
            //Arrange

            BuildSnippetInterpreter("\tCréer une zone mémoire #arc-en-ciel.", false);

            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should()
                .Be("Erreur, ligne 7:29 pas d'alternative viable à l'endroit ou il y a '-en'\n");
        }

        [Fact]
        private void TestEmptyNumericVariable()
        {
            //Arrange

            BuildSnippetInterpreter("\tCréer une zone mémoire #null.\n\tCopier (#null+2) dans #null.");

            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should().Be("Erreur, ligne 8:9 La variable #null n'a pas de valeur définie\n");
        }

        [Fact]
        private void Test1LetterVariable()
        {
            //Arrange

            BuildSnippetInterpreter($"{BuildAllocationSnippet("#i", "3")}\n" +
                                    $"{BuildAfficherSnippet("var:#i-")}\n\tAfficher #i.\n{BuildAfficherSnippet("#i")}");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Match("var:3-33");
        }


        [Fact]
        public void TestNotificationWithChange()
        {
            //Arrange
            var variableRef = "#ref".AsCosmosVariable(12.AsCosmosNumber());
            var newVal = 5;
            var observed = false;
            var observed2 = false;

            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef) +
                                    BuildCopySnippet(newVal, variableRef.Name) +
                                    BuildAfficherSnippet(variableRef.Name.ToString()));

            parser.Variables.DataUpdated += () => { observed = true; };
            parser.Variables.DataUpdated += () => { observed2 = true; };
            observed.Should().BeFalse();
            observed2.Should().BeFalse();


            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            Thread.Sleep(500); //waits for notification thread to work
            observed.Should().BeTrue();
            observed2.Should().BeTrue();
        }

        [Fact]
        public void TestNotificationWithoutChange()
        {
            //Arrange
            var observed = false;
            var observed2 = false;

            BuildSnippetInterpreter(BuildAfficherSnippet("Hello"));

            parser.Variables.DataUpdated += () => { observed = true; };
            parser.Variables.DataUpdated += () => { observed2 = true; };
            observed.Should().BeFalse();
            observed2.Should().BeFalse();


            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            Thread.Sleep(200); //waits for notification thread to work
            observed.Should().BeFalse();
            observed2.Should().BeFalse();
        }
    }
}