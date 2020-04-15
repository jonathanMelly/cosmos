using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using interpreter;
using Xunit.Abstractions;

namespace test
{
    public abstract class AbstractInterpreterTest
    {
        protected TestConsole testConsole;
        protected interpreter.Interpreter interpreter;
        private ITestOutputHelper helper;
        
        const string ValidHeaderSnippet = "Auteur: Jonathan Strokee\n" +
                                             "Date: 27.03.2020\n" +
                                             "Entreprise: ETML\n" +
                                             "Description: Demonstration du langage cosmos,\n" +
                                             "ce langage esttodo extraordinaire";

        const string ValidStartSnippet = "Voici les ordres du programme DEMO_COSMOS à classer dans la bibliothèque DEMONSTRATION :";
        const string ValidEnd = "Fin de la transmission.";

        protected const string TrueCondition = "1 vaut 1";
        protected const string FalseCondition = "2 vaut 3";
        
        public AbstractInterpreterTest(ITestOutputHelper helper)
        {
            testConsole = new TestConsole(helper);
            this.helper = helper;
        }
        
        
        protected virtual void BuildFileInterpreter(string file)
        {
            interpreter= new Interpreter().ForFile(file).WithConsole(testConsole);
        }
        
        protected virtual void BuildSnippetInterpreter(string content,bool assertParsing = true)
        {
            string program = $"{ValidHeaderSnippet}\n{ValidStartSnippet}\n\t{content}\n{ValidEnd}";
            
            helper.WriteLine(program);
            
            interpreter = new Interpreter().ForSnippet(program).WithConsole(testConsole);

            using (var scope = new AssertionScope())
            {
                interpreter.Parse().Should().BeTrue();
                interpreter.ErrorListener.Errors.Should().BeEmpty();
            }
            
        }
        
        protected string BuildIfStatement(bool condition, List<bool> elsifs = null, bool? elsee = null)
        {
            var result = new StringBuilder();
            const string function = "Afficher";
            result.Append($"\tSi {(condition?TrueCondition:FalseCondition)} alors\n\t\t{function} \"{(condition?TrueCondition:FalseCondition)}\".\n\t");
            if (elsifs != null)
            {
                foreach (var elsif in elsifs)
                {
                    result.Append($"sinon si {(elsif?TrueCondition:FalseCondition)} alors\n\t\t{function} \"{(elsif?TrueCondition:FalseCondition)}\".\n\t");
                }
            }

            if (elsee != null)
            {
                result.Append($"et sinon\n\t\t{function} \"{TrueCondition}\".\n\t");
            }

            result.Append("?\n");

            string resultString = result.ToString();
            
            //helper.WriteLine(resultString);

            return resultString;
        }
    }
}