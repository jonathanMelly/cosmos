using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using interpreter;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Xunit.Abstractions;
using Console = System.Console;

namespace test
{
    public class TestProgram
    {
        private readonly TestConsole fakeConsole;
        
        public TestProgram(ITestOutputHelper helper)
        {
            fakeConsole = new TestConsole(helper);
            Console.SetOut(fakeConsole);
        }
        
        [Fact]
        public void TestProgramWithValidFile()
        {
            //Arrange

            //Act
            interpreter.Program.Main(new[] {TestInterpreter.ValidProgramFile});
            
            //Assert
            Assert.Matches(TestInterpreter.ValidExecutionContent,fakeConsole.Content);
        }
    }

    
}