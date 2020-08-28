using FluentAssertions;
using lib.extension;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestSplit : AbstractInterpreterTest
    {
        public TestSplit(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestValidSplitInlineData()
        {
            //Arrange
            BuildSnippetInterpreter("\tDécouper \"a,b,c\" sur \",\".\n\tAfficher \"##decoupage.2 hello ##decoupage.3 ##decoupage.1\".");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Match("b hello c a");
        }

        [Fact]
        public void TestValidSplitVariables()
        {
            //Arrange
            var input = "#input".AsCosmosVariable("a,b,c".AsCosmosString());
            var separator = "#sep".AsCosmosVariable(",".AsCosmosString());
            BuildSnippetInterpreter( BuildAllocationSnippet(input) + "\n"+
                                     BuildAllocationSnippet(separator)+"\n"+
                                     $"\tDécouper \"{input.Name}\" sur \"{separator.Name}\".\n\tAfficher \"Test:##decoupage.2 ##decoupage.3 ##decoupage.1\".");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Match("Test:b c a");
        }

    }
}