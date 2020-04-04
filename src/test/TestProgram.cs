using System;
using System.IO;
using System.Text.RegularExpressions;
using interpreter;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace test
{
    public class TestProgram
    {
        private readonly StringWriter fakeConsole = new StringWriter();
        
        public TestProgram()
        {
            Console.SetOut(fakeConsole);
        }
        
        [Fact]
        public void TestProgramWithValidFile()
        {
            //Arrange

            //Act
            interpreter.Program.Main(new[] {TestInterpreter.ValidProgramFile});
            
            //Assert
            Assert.Matches(TestInterpreter.ValidExecutionContent,fakeConsole.ToString());
        }
    }
}