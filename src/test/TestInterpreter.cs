using System;
using interpreter;
using Xunit;

namespace test
{
    public class TestInterpreter
    {
        private const string Path = "../../../data/";

        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            var interpreter = new Interpreter(Path+"ValidProgram.cosmos");
            
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
