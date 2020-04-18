using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestSelection : AbstractInterpreterTest
    {
        public TestSelection(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestIfFalse1ElseIfTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(false,
                new List<bool> {true}));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }

        [Fact]
        public void TestIfFalseElseIfFalseElseTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(false,
                new List<bool> {false},
                true));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }

        [Fact]
        public void TestIfFalseElseIfTrueElseFalse()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(false,
                new List<bool> {true},
                false));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }

        [Fact]
        public void TestIfFalseElseTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(false,
                null, true));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }

        [Fact]
        public void TestIfFalseElsifFalseTrueFalseElseFalse()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(false,
                new List<bool>
                {
                    false, true, false
                },
                false));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }

        [Fact]
        public void TestIfTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(true));

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match(TrueCondition);
        }
    }
}