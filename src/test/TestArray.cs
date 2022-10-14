using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestArray : AbstractInterpreterTest
    {
        public TestArray(ITestOutputHelper helper) : base(helper)
        {
        }
        
        [Theory]
        [InlineData(0,45)]
        [InlineData("0",45)]
        [InlineData(0,"45")]
        [InlineData("0","45")]
        public void TestNumberStringArray(object index,object value)
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #array.",
                $"#array[{index}]={value}.",
                $"Afficher #array[{index}]."
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be(value.ToString());
        }
        
        [Fact]
        public void TestNestedIndexArray()
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #array.",
                "Créer #array2.",
                "#array[0]=38.",
                "#array2[#array[0]]=41.",
                "Afficher #array2[38]."
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be("41");
        }
        
        [Fact]
        public void TestArrayOfArray()
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #array.",
                "Créer #array2.",
                "#array2[\"fruit\"]=\"poire\".",
                "#array[0]=#array2.",
                "Créer #array3.",
                "#array3 = #array[0].",
                "Afficher #array3[\"fruit\"]."
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be("poire");
        }
    }
}