using FluentAssertions;
using interpreter;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestVariable : AbstractInterpreterTest
    {
        private const string Allocation = "Allouer la zone mémoire ";

        public TestVariable(ITestOutputHelper helper) : base(helper)
        {
            //Todo : allocation
            //Todo : affectation
            
        }

        [Fact]
        public void TestEmptyAllocationNumbered()
        {
            //Arrange
            var variable = new Variable("#0");
            BuildSnippetInterpreter(Allocation+variable.Name+".");
            
            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().HaveCount(1).And.ContainKey(variable.Name).WhichValue.Value.Should().BeNull();
        }
        
        [Fact]
        public void TestEmtpyAllocationNamed()
        {
            //Arrange
            var variable = new Variable("#maVariable");
            BuildSnippetInterpreter(BuildAllocationSnippet(variable));
            
            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().HaveCount(1).And.ContainKey(variable.Name).WhichValue.Value.Should().BeNull();
        }
        
        [Fact]
        public void TestStringAllocationVariant1()
        {
            TestAllocationWithValue("le texte","\"StringAllocation1\"");
        }
        
        [Fact]
        public void TestStringAllocationVariant2()
        {
            TestAllocationWithValue("","\"StringAllocation2\"");
        }
        
        [Fact]
        public void TestNumberAllocationVariant1()
        {
            TestAllocationWithValue("le nombre",55m);
        }
        
        [Fact]
        public void TestNumberAllocationVariant2()
        {
            TestAllocationWithValue("",66.6m);
        }
        
        [Fact]
        public void TestNumberExpressionAllocation()
        {
            TestAllocationWithValue("","1+2*3",7m);
        }
        
        [Fact]
        public void TestNumberWithParenthesesExpressionAllocation()
        {
            TestAllocationWithValue("","(1+2)*3",9m);
        }
        
        [Fact]
        public void TestRefToVariableAllocation()
        {
            //Arrange
            var variableRef = new Variable("#ref");
            variableRef.Value = 12m;
            
            var copy = new Variable("#copy");
            copy.Value = variableRef.Name;
            
            BuildSnippetInterpreter(BuildAllocationSnippet(variableRef)+"\t"+BuildAllocationSnippet(copy));
            

            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().
                HaveCount(2).
                And.ContainKey(variableRef.Name).And.ContainKey(copy.Name).
                WhichValue.Value.
                Should().Be(variableRef.Value).And.BeOfType(variableRef.Value.GetType());
        }

        private void TestAllocationWithValue(string prefix,object value,object expectedValue=null)
        {
            //Arrange
            var variable = new Variable("#maVariable");
            variable.Value = value;
            BuildSnippetInterpreter(BuildAllocationSnippet(variable,prefix));
            if (value is string)
            {
                value = value.ToString().Replace("\"","");
            }

            //Act
            interpreter.Execute().Should().BeTrue();
            
            //Assert
            interpreter.Variables.Should().
                HaveCount(1).
                And.ContainKey(variable.Name).
                    WhichValue.Value.
                        Should().Be(expectedValue?? value).
                            And.BeOfType(expectedValue!=null?expectedValue.GetType():value.GetType());
        }

        private static string BuildAllocationSnippet(Variable variable,string prefix="")
        {
            return $"{Allocation} {variable.Name} {(variable.Value!=null?$"avec {prefix} {variable.Value}":"")}.\n";
        }
    }
}