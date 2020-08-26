using System;
using FluentAssertions;
using lib.extension;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestRandom : AbstractInterpreterTest
    {
        public TestRandom(ITestOutputHelper helper) : base(helper)
        {
        }

        private string BuildRandomGeneration(int min, int max,string varName)
        {
            return $"\tPlacer un nombre aléatoire compris entre {min} et {max} dans {varName}.\n";
        }

        [Fact]
        public void TestRandomGeneration()
        {
            //Arrange
            const int min = 0;
            const int max = 5;
            var variable = "#random".AsCosmosVariable();
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+"\n"+ BuildRandomGeneration(min,max,variable.Name)+
                                    BuildAfficherSnippet(variable.Name));

            //Act
            interpreter.Execute();

            //Assert
            Convert.ToInt32(testConsole.Content).Should().BeGreaterOrEqualTo(min).And.BeLessOrEqualTo(max).
                And.Be(5); //test random have a fixed seed...
        }
    }
}