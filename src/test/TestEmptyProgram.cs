using FluentAssertions;
using lib.interpreter;
using lib.parser;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestEmptyProgram : AbstractInterpreterTest
    {
        public TestEmptyProgram(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestNoInstructions()
        {
            //Arrange - act - assert
            BuildSnippetInterpreter("",true);
        }

        [Fact]
        public void TestNoInstructionsWithoutLibraryHeader()
        {
            //Arrange
            var program = $"{Parser.BuildValidHeader("noheader")}{Parser.ValidEnd}";
            testConsole.Write(program);
            parser = new Parser().ForSnippet(program).WithConsole(testConsole);
            interpreter = new Interpreter(parser,testConsole).WithRandom(random);

            //act
            bool result = interpreter.Execute();

            //Assert
            result.Should().BeTrue();
        }
    }
}