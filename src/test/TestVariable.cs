using FluentAssertions;
using interpreter;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestVariable : AbstractInterpreterTest
    {
        public TestVariable(ITestOutputHelper helper) : base(helper)
        {
            //Todo : allocation
            //Todo : affectation
            
        }

        [Fact]
        public void TestAllocationNumbered()
        {
            //Arrange
            var variable = new Variable("#0");
            BuildSnippetInterpreter("Allouer la zone mémoire "+variable.Name+".");
            
            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().HaveCount(1).And.ContainKey(variable.Name);
        }
        
        [Fact]
        public void TestAllocationNamed()
        {
            //Arrange
            var variable = new Variable("#maVariable");
            BuildSnippetInterpreter("Allouer la zone mémoire "+variable.Name+".");
            
            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().HaveCount(1).And.ContainKey(variable.Name);
        }
    }
}