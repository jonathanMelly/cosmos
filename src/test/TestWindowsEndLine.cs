using FluentAssertions;
using lib.interpreter;
using lib.parser;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestWindowsEndLine : AbstractInterpreterTest
    {
        public TestWindowsEndLine(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        private void TestCrlfAnd4SpacesInsteadOfTab()
        {
            //Arrange
            var program = $"{Parser.BuildValidHeader("name", newLine: "\r\n")}    Afficher \"crlf\".\r\n{Parser.ValidEnd}";
            parser = new Parser().ForSnippet(program).WithConsole(testConsole);
            interpreter = new Interpreter(parser).WithRandom(random);

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Match("crlf");
        }
    }
}