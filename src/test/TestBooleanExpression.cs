using FluentAssertions;
using FluentAssertions.Execution;
using lib.extension;
using lib.parser.type;
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

        [Fact]
        public void TestEqual()
        {
            for (var i = 0; i < Tokens.IsEqual.Length; i++)
            {
                TestBoolean(True.IsEqualTo(i).True(),true);
                TestBoolean(False.IsEqualTo(i).False(),true);
                TestBoolean(False.IsEqualTo(i).True(),false);
            }

        }

        [Fact]
        public void TestDifferentBool()
        {
            for (var i = 0; i < Tokens.IsDifferent.Length; i++)
            {
                TestBoolean(True.IsDifferentThan(True,i), false);
                TestBoolean(False.IsDifferentThan(False,i), false);
                TestBoolean(False.IsDifferentThan(True,i), true);
            }
        }

        [Fact]
        public void TestDifferentString()
        {
            TestBoolean("\"5\"".IsDifferentThan("\"6\""), true);
        }

        [Fact]
        public void TestDifferentNumber()
        {
            TestBoolean("5".IsDifferentThan("6"), true);
        }

        [Fact]
        public void TestLeResultatDe()
        {
            TestBoolean("le résultat de (vrai et faux)".IsDifferentThan("vrai"), true);
        }

        [Fact]
        public void TestBigger()
        {
            for (var i = 0; i < Tokens.Gt.Length; i++)
            {
                TestBoolean("5".Gt("1",i), true);
                TestBoolean("1".Gt("5",i), false);
                TestBoolean("5".Gt("5",i), false);
            }
        }

        [Fact]
        public void TestLower()
        {
            for (var i = 0; i < Tokens.Lt.Length; i++)
            {
                TestBoolean("5".Lt("1",i), false);
                TestBoolean("1".Lt("5",i), true);
                TestBoolean("5".Lt("5",i), false);
            }
        }

        [Fact]
        public void TestBiggerEquals()
        {
            for (var i = 0; i < Tokens.Gte.Length; i++)
            {
                TestBoolean("5".Gte("1",i), true);
                TestBoolean("1".Gte("5",i), false);
                TestBoolean("5".Gte("5",i), true);
            }
        }

        [Fact]
        public void TestBiggerEqualsWrong()
        {
            //Arrange
            BuildSnippetInterpreter(BuildAllocationSnippet("#wrong",True.Gte("1")),false);

            //Act
            interpreter.Execute().Should().BeFalse();

            //Assert
            parser.Errors.Should().HaveCount(1).And.ContainMatch($"*élément invalide '{Tokens.Gte[0]}'*");
        }

        [Fact]
        public void TestLowerEquals()
        {
            for (var i = 0; i < Tokens.Lte.Length; i++)
            {
                TestBoolean("5".Lte("1",i), false);
                TestBoolean("1".Lte("5",i), true);
                TestBoolean("5".Lte("5",i), true);
            }
        }



        private void TestBoolean(string expression, bool expectedResult)
        {
            //Arrange
            var variable = new CosmosVariable($"#test",expectedResult.AsCosmosBoolean());
            BuildSnippetInterpreter(BuildAllocationSnippet(variable.Name,expression));

            //Act
            using (new AssertionScope())
            {
                interpreter.Execute().Should().BeTrue();
                parser.Errors.Should().BeEmpty();
            }


            //Assert
            parser.Variables.
                Should().ContainKey(variable.Name).
                WhichValue.Should().BeEquivalentTo(variable,expression+" should be "+expectedResult);
        }

    }
}