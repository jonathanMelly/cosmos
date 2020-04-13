using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestSelection : AbstractInterpreterTest
    {
        public TestSelection(ITestOutputHelper helper) : base(helper)
        {
        }
        
        [Fact]
        public void TestIfSingleCondition()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("4 vaut 4","1")));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfElseIfSingleCondition()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("1 vaut 3","0"),
                new List<Tuple<string, string>>(){Tuple.Create<string, string>("2 vaut 2","1")}));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfElseSingleCondition()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("1 vaut 3","0"),
                null,"1"));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfElseIfElseSingleConditionElseTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("1 vaut 3","0"),
                new List<Tuple<string, string>>(){Tuple.Create<string, string>("2 vaut 3","0")},
                "1"));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfElseIfElseSingleConditionIfElseTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("1 vaut 3","0"),
                new List<Tuple<string, string>>(){Tuple.Create<string, string>("2 vaut 2","1")},
                "0"));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }
        
        [Fact]
        public void TestIfElseIfElseSingleConditionSecondIfElseTrue()
        {
            //Arrange
            BuildSnippetInterpreter(BuildIfStatement(Tuple.Create("1 vaut 3","0"),
                new List<Tuple<string, string>>()
                {
                    Tuple.Create<string, string>("2 vaut 3","0"),
                    Tuple.Create<string, string>("2 vaut 2","1"),
                    Tuple.Create<string, string>("2 vaut 3","0")
                },
                "0"));
            
            //Act
            interpreter.Execute();
            
            //Assert
            testConsole.Content.Should().Match("1");
        }

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

        string BuildIfStatement(Tuple<string,string> condition, List<Tuple<string,string>> elsifs = null, string elsee = null)
        {
            var result = new StringBuilder();
            const string function = "Afficher";
            result.Append($"\tSi {condition.Item1} alors\n\t\t{function} {condition.Item2}.\n\t");
            if (elsifs != null)
            {
                foreach (var elsif in elsifs)
                {
                    result.Append($"sinon si {elsif.Item1} alors\n\t\t{function} {elsif.Item2}.\n\t");
                }
            }

            if (elsee != null)
            {
                result.Append($"et sinon\n\t\t{function} {elsee}.\n\t");
            }

            result.Append("?\n");
            

            return result.ToString();
        }

        
    }
}