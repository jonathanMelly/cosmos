using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestMath : AbstractInterpreterTest
    {
        public TestMath(ITestOutputHelper helper) : base(helper)
        {
        }
        
        [Fact]
        public void TestSqrt()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," racine carrée de  4"));
            
            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Value.Should().Be(2);
        }
    }
}