using FluentAssertions;
using interpreter;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestInterpreterGlobalWithFile : AbstractInterpreterTest
    {
        
        public const string ValidExecutionContent = 
            "En programmant avec cosmos, on peut\n" +
            "->Éxécuter du code avec une condition\n" +
            "-->dans laquelle on peut aussi avoir une condition (et ainsi de suite)\n" +
            "-->pour laquelle on peut avoir une alternative spécifique\n" +
            "-->pour laquelle on peut avoir une alternative si aucune condition préalable n'est vraie\n" +
            "->Afficher un nombre, par exemple quarante-quatre : 44";
        
        private const string Path = "../../../data/";
        public const string ValidProgramFile = Path +"ValidProgram.cosmos";
        public const string InvalidDateProgramFile = Path + "MissingDate.cosmos";

        public TestInterpreterGlobalWithFile(ITestOutputHelper helper) : base(helper)
        {
            //
        }


        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            BuildFileInterpreter(ValidProgramFile);
            
            //Act
            bool success = interpreter.Parse();
            
            //Assert
            Assert.True(success,"There should be no parse error");
        }
        
        [Fact]
        public void TestInvalidProgramMissingDate()
        {
            //Arrange
            BuildFileInterpreter(InvalidDateProgramFile);
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
            BuildFileInterpreter(ValidProgramFile);

            //Act
            interpreter.Execute();
            
            //Assert
            interpreter.Errors.Should().BeEmpty();
            testConsole.Content.Should().Match(ValidExecutionContent);

        }

        

    }
}
