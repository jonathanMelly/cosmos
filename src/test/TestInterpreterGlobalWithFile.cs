using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestInterpreterGlobalWithFile : AbstractInterpreterTest
    {
        public TestInterpreterGlobalWithFile(ITestOutputHelper helper) : base(helper)
        {
            //
        }

        public const string ValidExecutionContent =
            "En programmant avec cosmos, on peut\n" +
            "->Éxécuter du code avec une condition\n" +
            "-->dans laquelle on peut aussi avoir une condition (et ainsi de suite)\n" +
            "-->pour laquelle on peut avoir une alternative spécifique\n" +
            "-->pour laquelle on peut avoir une alternative si aucune condition préalable n'est vraie\n" +
            "->Afficher un nombre, par exemple quarante-quatre : 44";


        public static readonly string ValidProgramFile = DataFilePath + "ValidProgram.cosmos";
        public static readonly string InvalidDateProgramFile = DataFilePath + "MissingDate.cosmos";

        [Fact]
        public void TestExecuteValidProgram()
        {
            //Arrange
            BuildFileInterpreter(ValidProgramFile);

            //Act
            interpreter.Execute();

            //Assert
            parser.Errors.Should().BeEmpty();
            testConsole.Content.Should().Match(ValidExecutionContent);
        }

        [Fact]
        public void TestInvalidProgramMissingDate()
        {
            //Arrange
            BuildFileInterpreter(InvalidDateProgramFile);
            var dateError = "attendu 'Date:'";

            //Act
            interpreter.Execute();

            //Assert
            testConsole.ErrorContent.Should().Contain(dateError); //this could go into ErrorListener test...
        }


        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            BuildFileInterpreter(ValidProgramFile);

            //Act
            var success = parser.Parse();

            //Assert
            Assert.True(success, "There should be no parse error");
        }

        [Fact]
        public void TestConsoleManipulation()
        {
            //Arrange
            BuildFileInterpreter(DataFilePath+"ConsoleManipulation.cosmos");

            //Act
            interpreter.Execute();

            //Assert
            testConsole.Content.Should().Match("@@Set cursor y to 5\n@@Set cursor x to 6\n@@Set back color to DarkRed\n@@Set front color to Blue\nTexte en couleur et décalé");
        }
    }
}