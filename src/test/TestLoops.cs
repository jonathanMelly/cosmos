using FluentAssertions;
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
    }
}