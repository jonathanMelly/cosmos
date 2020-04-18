using Xunit.Abstractions;

namespace test
{
    public class TestBooleanExpression : AbstractInterpreterTest
    {
        public TestBooleanExpression(ITestOutputHelper helper) : base(helper)
        {
            //
        }

        //TODO tests
/*
        [Fact]
        public void TestIfTrueAnd()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 4 et 5 vaut 5","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfTrueAnd2()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 4 et 5 vaut 5 et 6 vaut 6","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfFalseAnd()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 4 et 5 vaut 5 et 6 vaut 7","0")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().BeEmpty();
        }
        
        [Fact]
        public void TestIfOrTrue1()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 4 ou 5 vaut 5 ou 6 vaut 7","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfOrTrue2()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 5 ou 5 vaut 3 ou 7 vaut 7","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfOrFalse()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 5 ou 5 vaut 3 ou 6 vaut 7","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().BeEmpty();
        }
        
        [Fact]
        public void TestIfXor()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 3 ou au contraire 5 vaut 5","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfAndOrMixedNaturalOrder()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 3 ou 5 vaut 5 et 0 vaut 1","0")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().BeEmpty();
        }
        
        [Fact]
        public void TestIfAndOrMixedBadOrder()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 3 et 5 vaut 4 ou 1 vaut 1","0")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().BeEmpty();
        }
        
        */
    }
}