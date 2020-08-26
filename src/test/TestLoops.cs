using FluentAssertions;
using lib.extension;
using lib.parser.type;
using test.extension;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestLoops : AbstractInterpreterTest
    {
        public TestLoops(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestValidLoopParsing()
        {
            //Arrange
            BuildFileInterpreter(DataFilePath+"/ValidLoops.cosmos");

            //Act-assert
            parser.Parse().Should().BeTrue();
            parser.Errors.Should().BeEmpty();
        }

        [Fact]
        public void TestStaticLoop5Times()
        {
            //Arrange
            var varName = "#counter";
            var variable = new CosmosVariable(varName,0.AsCosmosNumber());
            const int iterations = 5;
            BuildSnippetInterpreter(BuildAllocationSnippet(variable) +
                                    BuildStaticLoopSnippet(iterations,$"\t{varName} = {varName} +1."));

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables.Should().ContainKey(varName).WhichValue.Value.Should().Be(5.AsCosmosNumber());
        }

        [Fact]
        public void TestDynamicLoop1()
        {
            //Arrange
            var total = 5.AsCosmosNumber();
            var varName = "#counter";
            var variable = new CosmosVariable(varName,0.AsCosmosNumber());
            var iterations = new CosmosVariable($"#loops",total);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    BuildDynamicLoopSnippet(iterations,$"\t{varName} = {varName} +1.")+
                                    BuildAfficherSnippet(varName));

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables.Should().ContainKey(varName).WhichValue.Value.Should().BeEquivalentTo(total);
            testConsole.Content.Should().Be(total.ToString());
        }

        [Fact]
        public void TestDynamicLoop2()
        {
            //Arrange
            var total = 6.AsCosmosNumber();
            var varName = "#counter";
            var variable = new CosmosVariable(varName,0.AsCosmosNumber());
            var iterations = new CosmosVariable($"#loops",total);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    BuildDynamicLoopSnippet(iterations,$"\t{varName} = {varName} +1.",1)+
                                    BuildAfficherSnippet(varName));

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables.Should().ContainKey(varName).WhichValue.Value.Should().BeEquivalentTo(total);
            testConsole.Content.Should().Be(total.ToString());

        }

        [Fact]
        public void TestWhileLoopInlineFalseBoolExpression()
        {
            //Arrange
            BuildSnippetInterpreter(BuildWhileLoopSnippet("1".Gt("2"),BuildAfficherSnippet("error")));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().BeEmpty();

        }

        [Fact]
        public void TestWhileLoopVariableBoolExpression()
        {
            //Arrange
            const string printedResult = "1";
            var condition = true.AsCosmosBoolean();
            var varName = "#condition";
            var variable = new CosmosVariable(varName,condition);

            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    BuildWhileLoopSnippet(varName,
                                        $"\t\t{varName}={!condition}.\n" +
                                                $"\t{BuildAfficherSnippet(printedResult)}")+
                                    BuildAfficherSnippet(varName));

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables.Should().ContainKey(varName).WhichValue.Value.Should().BeEquivalentTo(!condition);
            testConsole.Content.Should().Be(printedResult+false.AsCosmosBoolean());

        }
    }
}