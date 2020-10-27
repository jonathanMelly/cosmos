using System;
using FluentAssertions;
using lib.extension;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestMath : AbstractInterpreterTest
    {
        public TestMath(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public void TestSqrt()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," racine carrée de  4"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Value.Should().Be(2);
        }

        [Fact]
        public void TestModuloV1()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," le reste de la division entière de 5 par 2"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Value.Should().Be(1);
        }

        [Fact]
        public void TestModuloV2()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," 4 modulo 3"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Value.Should().Be(1);
        }

        [Fact]
        public void TestModuloV3()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," 4 % 3"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Value.Should().Be(1);
        }
        
        [Fact]
        public void TestSinus()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName,"  sin 3 == le sinus de 3"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Boolean().Value.Should().Be(true);
        }
        
        [Fact]
        public void TestCosinus()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," cos 3 == le cosinus de 3"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Boolean().Value.Should().Be(true);
        }
        
        [Fact]
        public void TestPi()
        {
            //arrange
            var varName = "#number";
            BuildSnippetInterpreter(BuildAllocationSnippet(varName," pi"));

            //act
            interpreter.Execute().Should().BeTrue();

            //assert
            parser.Variables[varName].Value.Number().Should().Be(Math.PI.AsCosmosNumber());
        }
    }
}