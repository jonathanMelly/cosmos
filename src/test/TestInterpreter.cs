using System;
using interpreter;
using Xunit;

namespace test
{
    public class TestInterpreter
    {
        public const string Path = "../../../data/";
        public const string ValidProgramFile = Path +"ValidProgram.cosmos";

        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            var interpreter = new Interpreter(ValidProgramFile);
            
            //Act
            bool hasError = interpreter.Parse();
            
            //Assert
            Assert.False(hasError,"There should be no parse error");
        }
        
        [Fact]
        public void TestParsingOnInvalidProgramMissingDate()
        {
            //Arrange
            var interpreter = new Interpreter(Path+"MissingDate.cosmos");
            
            //Act
            interpreter.Parse();
            
            
            //Assert
            Assert.Contains(interpreter.Errors,error => error.Contains("expecting 'Date:'"));
        }
    }
}
