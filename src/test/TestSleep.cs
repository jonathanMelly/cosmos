using System.Diagnostics;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestSleep : AbstractInterpreterTest
    {
        public TestSleep(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestSleep500()
        {
            //Arrange
            var stopWatch = new Stopwatch();
            BuildSnippetInterpreter("\tAttendre 500ms.");

            //Act
            stopWatch.Start();
            interpreter.Execute();
            stopWatch.Stop();

            //Assert
            stopWatch.Elapsed.Milliseconds.Should().BeGreaterOrEqualTo(500);
        }
    }
}