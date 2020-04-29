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
    }
}