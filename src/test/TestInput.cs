using FluentAssertions;
using lib.extension;
using lib.parser.type;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestInput : AbstractInterpreterTest
    {


        public TestInput(ITestOutputHelper helper) : base(helper)
        {

        }

        [Fact]
        public void TestIntInput()
        {
            TestInputGeneric(55.AsCosmosNumber());
        }

        [Fact]
        public void TestDoubleInput()
        {
            TestInputGeneric(55.67.AsCosmosNumber());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestBoolInput(bool param)
        {
            TestInputGeneric(param.AsCosmosBoolean());
            TestInputGeneric(param.AsCosmosBoolean());
        }

        [Theory]
        [InlineData("hello")]
        [InlineData("avec un espace")]
        public void TestStringInput(string param)
        {
            TestInputGeneric(param.AsCosmosString());
        }

        private void TestInputGeneric(CosmosTypedValue expected)
        {

            //Arrange
            var variable = new CosmosVariable("#input",null);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    BuildInputSnippet(variable));
            testConsole.Input.Push($"{expected}");

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables[variable.Name].Value.Should().Be(expected);
        }
    }
}