using FluentAssertions;
using interpreter;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestInterpreter
    {
        private TestConsole testConsole;
        public const string ValidExecutionContent = 
            "En programmant avec cosmos, on peut\n" +
            "->Éxécuter du code avec une condition\n" +
            "-->dans laquelle on peut aussi avoir une condition (et ainsi de suite)\n" +
            "-->pour laquelle on peut avoir une alternative spécifique\n" +
            "-->pour laquelle on peut avoir une alternative si aucune condition préalable n'est vraie\n" +
            "->Afficher un nombre, par exemple quarante-quatre : 44";
        
        public const string Path = "../../../data/";
        public const string ValidProgramFile = Path +"ValidProgram.cosmos";
        public const string InvalidDateProgramFile = Path + "MissingDate.cosmos";

        public TestInterpreter(ITestOutputHelper helper)
        {
            testConsole = new TestConsole(helper);
        }

        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            var interpreter = BuildFileInterpreter(ValidProgramFile);
            
            //Act
            bool success = interpreter.Parse();
            
            //Assert
            Assert.True(success,"There should be no parse error");
        }
        
        [Fact]
        public void TestInvalidProgramMissingDate()
        {
            //Arrange
            var interpreter = BuildFileInterpreter(InvalidDateProgramFile);
            string dateError = "expecting 'Date:'";
            
            //Act
            interpreter.Execute();

            //Assert
            Assert.Contains(interpreter.Errors,error => error.Contains(dateError));
            testConsole.ErrorContent.Should().Contain(dateError);//this could go into ErrorListener test...
        }

        [Fact]
        public void TestExecuteValidProgram()
        {
            //Arrange
            var interpreter = BuildFileInterpreter(ValidProgramFile);

            //Act
            interpreter.Execute();
            
            //Assert
            interpreter.Errors.Should().BeEmpty();
            testConsole.Content.Should().Match(ValidExecutionContent);

        }

        private Interpreter BuildFileInterpreter(string file)
        {
            return new Interpreter().ForFile(file).WithConsole(testConsole);
        }
        
        private Interpreter BuildSnippetInterpreter(string file)
        {
            return new Interpreter().ForSnippet(file).WithConsole(testConsole);
        }

    }
}
