using commandline_tool;
using FluentAssertions;
using Xunit;

namespace test
{
    public class TestStringExtension
    {
        [Fact]
        public void TestIsMatch()
        {
            //Arrange
            var cmd = "--help";

            //Act
            bool result = cmd.IsMatch(Program.OptHelp);

            //Assert
            result.Should().BeTrue("la ligne de commande contient le paramètre --help");
        }

        [Fact]
        public void TestIsMatchNegative()
        {
            //Arrange
            var cmd = "--helpo";

            //Act
            bool result = cmd.IsMatch(Program.OptHelp);

            //Assert
            result.Should().BeFalse();
        }
    }
}