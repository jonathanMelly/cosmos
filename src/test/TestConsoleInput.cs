using System.Threading;
using FluentAssertions;
using lib.extension;
using lib.parser.type;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestInput : AbstractInterpreterTest
    {


        public TestInput(ITestOutputHelper helper) : base(helper)
        {

        }

        [Fact]
        public void TestIntInput()
        {
            TestInputGeneric(55.AsCosmosNumber());
        }

        [Fact]
        public void TestDoubleInput()
        {
            TestInputGeneric(55.67.AsCosmosNumber());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestBoolInput(bool param)
        {
            TestInputGeneric(param.AsCosmosBoolean());
            TestInputGeneric(param.AsCosmosBoolean());
        }

        [Theory]
        [InlineData("hello")]
        [InlineData("avec un espace")]
        public void TestStringInput(string param)
        {
            TestInputGeneric(param.AsCosmosString());
        }

        private void TestInputGeneric(CosmosTypedValue expected)
        {

            //Arrange
            var variable = new CosmosVariable("#input",null);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    BuildInputSnippet(variable));
            testConsole.Input.Push($"{expected}");

            //Act
            interpreter.Execute();

            //Assert
            parser.Variables[variable.Name].Value.Should().Be(expected);
        }

        [Fact]
        private void TestDirectInputEmpty()
        {

            //Arrange
            var variable = new CosmosVariable("#input",null);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    "\n\t#input = ##touche .\n"+BuildAfficherSnippet("touche:##touche"));

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables[variable.Name].Value.Should().BeNull();
            testConsole.Content.Should().Match("touche:<NÉANT>");
        }

        [Fact]
        private void TestDirectInputFilled()
        {

            //Arrange
            var variable = new CosmosVariable("#input",null);
            testConsole.Input.Push("A");
            testConsole.Input.Push("B");
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    "\n\t#input = ##touche .\n"+
                                    BuildAfficherSnippet("#input")+
                                    BuildAfficherSnippet("##touche")+
                                    BuildAfficherSnippet("##touche"));

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            testConsole.Content.Should().Match("BA<NÉANT>");
        }

        [Fact]
        private void TestDirectInputAvailable()
        {

            //Arrange
            var variable = new CosmosVariable("#available",null);
            testConsole.Input.Push("A");
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    $"\n\t{variable.Name} = ##touche.disponible .\n");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables[variable.Name].Value.Should().Be(true.AsCosmosBoolean());
        }

        [Fact]
        private void TestDirectInputNotAvailable()
        {

            //Arrange
            var variable = new CosmosVariable("#available",null);
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    $"\n\t{variable.Name} = ##touche.disponible .\n");

            //Act
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables[variable.Name].Value.Should().Be(false.AsCosmosBoolean());
        }

        [Fact]
        private void TestFakeReadKey()
        {

            //Arrange
            var variable = new CosmosVariable("#press",null);
            //testConsole.Input.Push("A");
            BuildSnippetInterpreter(BuildAllocationSnippet(variable)+
                                    $"\n\tAttendre la prochaine touche et la stocker dans {variable.Name} .\n");

            //Act
            new Thread(() =>
            {
                Thread.Sleep(800);
                testConsole.Input.Push("A");
            }).Start();
            interpreter.Execute().Should().BeTrue();

            //Assert
            parser.Variables[variable.Name].Value.Should().Be("A".AsCosmosString());
        }
    }
}