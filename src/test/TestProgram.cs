using System;
using commandline_tool;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestProgram
    {
        public TestProgram(ITestOutputHelper helper)
        {
            fakeConsole = new XUnitCompatibleConsole(helper);
            Console.SetOut(fakeConsole);
            Console.SetError(fakeConsole);
        }

        private readonly XUnitCompatibleConsole fakeConsole;

        [Fact]
        public void TestProgramWithInvalidFile()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main( new String[]{TestInterpreterGlobalWithFile.InvalidDateProgramFile,"--direct"});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.ErreurSyntaxe);
            fakeConsole.Content.Should().Contain("attendu 'Date:'").And.Contain("il manque 'Voici les ordres du programme' à l'endroit ou il y a 'Voici'");
        }

        [Fact]
        public void TestProgramWithValidFile()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main(new String[]{TestInterpreterGlobalWithFile.ValidProgramFile,"-d"});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.Ok);
            fakeConsole.Content.Should().Match(TestInterpreterGlobalWithFile.ValidExecutionContent);
        }

        [Fact]
        public void TestHelp()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main(new String[]{"-h"});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.Ok);
            fakeConsole.Content.Should().Contain("aide");
        }

        [Fact]
        public void TestBadArg()
        {
            //Arrange
            int exitCode;

            //Act
            exitCode = Program.Main(new String[]{"-h5"});

            //Assert
            exitCode.Should().Be((int) Program.ExitCode.ArgumentInvalide);
            fakeConsole.Content.Should().Contain("Erreur");
        }
    }
}