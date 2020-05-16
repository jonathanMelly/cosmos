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

            BuildSnippetInterpreter(BuildAllocationSnippet(variableName, variableExpression, variablePrefix));

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables.Should().HaveCount(1).And.ContainKey(expectedResult.Name).WhichValue.Should()
                .BeEquivalentTo(expectedResult);
        }

        private void TestInValidAllocationWithValue(string variableExpression, string variablePrefix = "")
        {
            //Arrange
            var variableName = "#maVariable";

            BuildSnippetInterpreter(BuildAllocationSnippet(variableName, variableExpression, variablePrefix), false);

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
                                    BuildAllocationSnippet(copy.Name, variableRef.Name+$"Error"));


            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            testConsole.ErrorContent.Should().Contain("inconnue");
        }

        [Fact]
        public void TestStringAllocationVariant1BadPrefix()
        {
            var value = "StringAllocation1BadPrefix";
            TestInValidAllocationWithValue($"\"{value}\"", "le exte");
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
    }
}