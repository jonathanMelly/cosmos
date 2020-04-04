using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using interpreter;
using Xunit;
using FluentAssertions;

namespace test
{
    public class TestInterpreter
    {
        public const string ValidExecutionContent = "salut44";
        public const string Path = "../../../data/";
        public const string ValidProgramFile = Path +"ValidProgram.cosmos";

        [Fact]
        public void TestParsingOnValidProgram()
        {
            //Arrange
            var interpreter = new Interpreter(ValidProgramFile);
            
            //Act
            bool success = interpreter.Parse();
            
            //Assert
            Assert.True(success,"There should be no parse error");
        }
        
        [Fact]
        public void TestInvalidProgramMissingDate()
        {
            //Arrange
            var interpreter = new Interpreter(Path+"MissingDate.cosmos");
            
            //Act
            interpreter.Execute();

            //Assert
            Assert.Contains(interpreter.Errors,error => error.Contains("expecting 'Date:'"));
        }

        [Fact]
        public void TestExecuteValidProgram()
        {
            //Arrange
            var interpreter = new Interpreter(ValidProgramFile);
            var fakeConsole = new StringWriter();
            Console.SetOut(fakeConsole);
            
            //Act
            interpreter.Execute();
            
            //Assert
            Assert.Matches(ValidExecutionContent,fakeConsole.ToString());
        }

    }
}
