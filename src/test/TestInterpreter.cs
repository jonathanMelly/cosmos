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
        
        [Fact (Skip = "Better done directly on pipeline using process exit code")]
        //Inspired from https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.outputdatareceived?view=netcore-3.1
        public void IntegrationTest()
        {
        
            //Arrange
            var output = new StringBuilder();
            
            Process process = new Process();
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments="run --project ../../../../interpreter/interpreter.csproj "+ValidProgramFile;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            
            var dreh = new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    output.Append(e.Data);
                }
            });
            process.OutputDataReceived += dreh;
            process.ErrorDataReceived += dreh;
            
            //Act
            process.Start();

            // Asynchronously read the standard output of the spawned process. 
            // This raises OutputDataReceived events for each line of output.
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit(10000);
            process.Close();

            //Assert
            output.ToString().Should().Match(ValidExecutionContent);
        }
        
    }
}
