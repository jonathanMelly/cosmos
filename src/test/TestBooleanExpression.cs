using FluentAssertions;
using FluentAssertions.Execution;
using interpreter;
using interpreter.extensions;
using test.extension;
using Xunit;
using Xunit.Abstractions;
using static  test.Tokens;

namespace test
{
    public class TestBooleanExpression : AbstractInterpreterTest
    {
        
        
        public TestBooleanExpression(ITestOutputHelper helper) : base(helper)
        {
            //
        }
        

        
        [Fact]
        public void TestAndAllTrue()
        {
            TestBoolean(True.And().True2().And().True3(),true);
        }
        
        [Fact]
        public void TestAndAllFalse()
        {
            TestBoolean(False.And().False2().And().False3(),false);
        }
        
        [Fact]
        public void TestFalseAndMixedFirstFalse()
        {
            TestBoolean(False.And().True().And().True2(),false);
        }
        
        [Fact]
        public void TestFalseAndMixedFirstTrue()
        {
            TestBoolean(True.And().True2().And().False3(),false);
        }
        
        [Fact]
        public void TestOrAllTrue()
        {
            TestBoolean(True.Or().Group(True2.Or().True3()),true);
        }
        
        [Fact]
        public void TestOrAllFalse()
        {
            TestBoolean(False.Or().Group(False2.Or().False3()),false);
        }
        
        [Fact]
        public void TestOrFirstTrue()
        {
            TestBoolean(True.Or().False2().Or().False3(),true);
            TestBoolean("vrai ou KO ou (2 == 1)",true);
        }
        
        [Fact]
        public void TestOrFirstFalse()
        {
            TestBoolean(False.Or().False2().Or().True3(),true);
        }

        [Fact]
        public void TestXorTrue()
        {
            TestBoolean(True.Xor().False(),true);
        }
        
        [Fact]
        public void TestXorFalse()
        {
            TestBoolean(True.Xor().True(),false);
        }
        
        [Fact]
        public void TestAndOrMixed1()
        {
            TestBoolean("".Group(False.Or().True()).And().False(),false);
        }
        
        [Fact]
        public void TestAndOrMixed2()
        {
            TestBoolean("".Group(False.Or().True()).And().True(),true);
        }
        
        
        public void TestBoolean(string expression, bool expectedResult)
        {
            //Arrange
            var variable = new CosmosVariable($"#test",expectedResult.AsCosmosBoolean());
            BuildSnippetInterpreter(BuildAllocationSnippet(variable.Name,expression));
            
            //Act
            using (new AssertionScope())
            {
                interpreter.Execute().Should().BeTrue();
                interpreter.Errors.Should().BeEmpty();
            }
            
            
            //Assert
            interpreter.Variables.
                Should().ContainKey(variable.Name).
                WhichValue.Should().BeEquivalentTo(variable,expression+" should be "+expectedResult);
        }
        
    }
}