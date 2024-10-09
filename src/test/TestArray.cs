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
                "#array[1]=39.",
                "#array2[#array[0]]=41.",
                "Afficher #array2[38].",
                "Afficher \"-\".",
                "Afficher #array[0].",
                "Afficher #array[1].",
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be("41-3839");
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
        
        [Fact]
        public void TestDynamicIndex()
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #array.",
                "Créer #i avec la valeur 0.",
                "#array[#i]=44.",
                "Afficher \"la valeur est \".",
                "Afficher #array[#i].",
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be("la valeur est 44");
        }
        
        [Fact]
        public void TestArrayInStringInterpolate()
        {
            //Arrange
            BuildSnippetInterpreter(new[]
            {
                "Créer #array.",
                "Créer #i avec la valeur 0.",
                "#array[0]=44.",
                "#array[1]=45.",
                "Afficher \"la valeur est {#array[0]} \".",
                "Afficher \"{#array[#i]}\".",
                "Afficher \"-#array[1]\".",
            });
            
            //Act
            var executionResult = interpreter.Execute();

            //Assert
            executionResult.Should().BeTrue();
            testConsole.Content.Should().Be("la valeur est 44 44-45");
        }
    }
}