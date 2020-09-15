using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestCursor : AbstractInterpreterTest
    {
        public TestCursor(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestCursorX()
        {
            //Arrange
            BuildSnippetInterpreter("\tPlacer le curseur à la ligne 40.");

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match("@@Set cursor y to 40");
        }

        [Fact]
        public void TestCursorY()
        {
            //Arrange
            BuildSnippetInterpreter("\tPlacer le curseur à la colonne 40.");

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match("@@Set cursor x to 40");
        }

        [Fact]
        public void TestCursorVisibleTrue()
        {
            //Arrange
            BuildSnippetInterpreter("\tAfficher le curseur.");

            //Act
            interpreter.Execute();

            //Assert
            testConsole.CursorVisible.Should().BeTrue();
        }

        [Fact]
        public void TestCursorVisibleFalse()
        {
            //Arrange
            BuildSnippetInterpreter("\tMasquer le curseur.");

            //Act
            interpreter.Execute();

            //Assert
            testConsole.CursorVisible.Should().BeFalse();
        }

    }
}