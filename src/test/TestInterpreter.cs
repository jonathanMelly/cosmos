using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using interpreter;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;
using Console = System.Console;

namespace test
{
    public class TestInterpreter
    {
        private TestConsole testConsole;
        public const string ValidExecutionContent = "salut chef\n1 n'est pas égal à 2\n44";
        public const string Path = "../../../data/";
        public const string ValidProgramFile = Path +"ValidProgram.cosmos";

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
            var interpreter = BuildFileInterpreter(Path+"MissingDate.cosmos");
            
            //Act
            interpreter.Execute();

            //Assert
            Assert.Contains(interpreter.Errors,error => error.Contains("expecting 'Date:'"));
        }

        [Fact]
        public void TestExecuteValidProgram()
        {
            //Arrange
            var interpreter = BuildFileInterpreter(ValidProgramFile);

            //Act
            interpreter.Execute();
            
            //Assert
            Assert.Matches(ValidExecutionContent,testConsole.Content);
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
