using System;
using FluentAssertions;
using lib;
using lib.extension;
using Xunit;
using Xunit.Abstractions;

namespace test
{
    public class TestCalendar : AbstractInterpreterTest
    {
        public TestCalendar(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        private void TestDatePrint()
        {
            //Arrange
            BuildSnippetInterpreter(
                BuildAfficherSnippet(
                    "{##date.jour}.{##date.mois}.{##date.annee} ##date.heure:##date.minute:##date.seconde"
                ));
            var year = 2020;
            var month = 12;
            var day = 31;
            var hour = 23;
            var minute = 59;
            var second = "05";

            using (Clock.NowIs(new DateTime(year, month, day, hour, minute, Convert.ToInt32(second))))
            {
                //Act
                interpreter.Execute().Should().BeTrue();

                //Assert
                testConsole.Content.Should().Be($"{day}.{month}.{year} {hour}:{minute}:{second}");
            }
        }

        [Fact]
        private void TestDateAffectation()
        {
            //Arrange
            BuildSnippetInterpreter(
                BuildAllocationSnippet("#year","##date.annee")
                +BuildAllocationSnippet("#compute","#year+1")
                + BuildAllocationSnippet("#direct",$"##date.jour+2")
                +BuildAllocationSnippet("#res","-5")
                +"\tCopier le résultat de (##date.mois+2) dans #res.\n");
            var year = 2020;
            var month = 12;
            var day = 31;
            var hour = 23;
            var minute = 59;
            var second = "05";

            using (Clock.NowIs(new DateTime(year, month, day, hour, minute, Convert.ToInt32(second))))
            {
                //Act
                interpreter.Execute().Should().BeTrue();

                //Assert
                parser.Variables["#year"].Value.Should().Be(year.AsCosmosNumber());
                parser.Variables["#compute"].Value.Should().Be((year+1).AsCosmosNumber());
                parser.Variables["#direct"].Value.Should().Be((day+2).AsCosmosNumber());
                parser.Variables["#res"].Value.Should().Be((month+2).AsCosmosNumber());
            }
        }
    }
}