using System;
using FluentAssertions;
using lib;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestProgram
    {
        public TestProgram(ITestOutputHelper helper)
        {
            fakeConsole = new TestConsole(helper);
            Console.SetOut(fakeConsole);
            Console.SetError(fakeConsole);
        }

        private readonly TestConsole fakeConsole;

        [Fact]
        public void TestProgramWithInvalidFile()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main(new[] {TestInterpreterGlobalWithFile.InvalidDateProgramFile});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.CompileError);
            fakeConsole.Content.Should().Contain("attendu 'Date:'");
        }

        [Fact]
        public void TestProgramWithValidFile()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main(new[] {TestInterpreterGlobalWithFile.ValidProgramFile});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.Success);
            fakeConsole.Content.Should().Match(TestInterpreterGlobalWithFile.ValidExecutionContent);
        }
    }
}