using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestErrors : AbstractInterpreterTest
    {
        public TestErrors(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestEnhanceEnd()
        {
            //Arrange
            BuildSnippetInterpreter("\tSi 1 == 1 alors\n\t\tAfficher \"rien\".\n\tAfficher \"error\".\n",false);
            
            //Act
            interpreter.Execute().Should().BeFalse();
            
            //Assert
            testConsole.ErrorContent.Should().Contain("oublié");
        }
        
        [Fact]
        public void TestNoViable()
        {
            //Arrange
            BuildSnippetInterpreter("\tSir 1 == 1 alors\n\t\tAfficher \"rien\".\n\tAfficher \"error\".\n\tSi 1 == 1 alors\n\t\tAfficher \"rien\".\n\t?\n",false);
            
            //Act
            interpreter.Execute().Should().BeFalse();
            
            //Assert
            testConsole.ErrorContent.Should().Contain("viable");
        }
        
        [Fact]
        public void TestUseEmptyVarInLoop()
        {
            //Arrange
            BuildSnippetInterpreter("\tCréer #empty.\n\tRépéter #empty x\n\t\tAfficher \"no\".\n\t>>\n");
            
            //Act
            interpreter.Execute().Should().BeFalse();
            
            //Assert
            testConsole.ErrorContent.Should().Contain("pas de valeur définie");
        }
    }
}