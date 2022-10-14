using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestSpaces : AbstractInterpreterTest
    {
        public TestSpaces(ITestOutputHelper helper) : base(helper)
        {
        }
        
        [Fact]
        public void TestTabAfterPoint()
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #tabAfter.	",
                "Créer #tabSpaceAfter.	   ",
                "Si 1==2 alors	   ","Afficher 1.","?	   ",
            });
            
            //Act
            var result = interpreter.Execute();

            //Assert
            result.Should().BeTrue();
        }
    }
}