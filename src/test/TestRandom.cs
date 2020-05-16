using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestRandom : AbstractInterpreterTest
    {
        public TestRandom(ITestOutputHelper helper) : base(helper)
        {
        }

        private string BuildRandomExpression(int min, int max)
        {
            return $"un nombre aléatoire entre {min} et {max}";
        }

        [Fact]
        public void TestRandomAllocation()
        {
            //Arrange
            const int min = 0;
            const int max = 5;
            const string varName = "#random";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName,BuildRandomExpression(min,max))+
                                    BuildAfficherSnippet(varName));

            //Act
            interpreter.Execute();

            //Assert
            Convert.ToInt32(testConsole.Content).Should().BeGreaterOrEqualTo(min).And.BeLessOrEqualTo(max).
                And.Be(5); //test random have a fixed seed...
        }

        [Fact]
        public void TestRandomAffectation()
        {
            //Arrange
            const int min = 10;
            const int max = 15;
            const string varName = "#random";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName,BuildRandomExpression(min,max))+
                                    $"\t{varName}={BuildRandomExpression(min,max)}.\n"+
                                    BuildAfficherSnippet(varName));

            //Act
            interpreter.Execute();

            //Assert
            Convert.ToInt32(testConsole.Content).Should().BeGreaterOrEqualTo(min).And.BeLessOrEqualTo(max).
                And.Be(15); //test random have a fixed seed...
        }
    }
}