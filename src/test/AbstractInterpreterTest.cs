using interpreter;
using Xunit.Abstractions;

namespace test
{
    public abstract class AbstractInterpreterTest
    {
        protected TestConsole testConsole;
        protected interpreter.Interpreter interpreter;
        
        const string ValidHeaderSnippet = "Auteur: Jonathan Strokee\n" +
                                             "Date: 27.03.2020\n" +
                                             "Entreprise: ETML\n" +
                                             "Description: Demonstration du langage cosmos,\n" +
                                             "ce langage est extraordinaire";

        const string ValidStartSnippet = "Voici les ordres du programme DEMO_COSMOS à classer dans la bibliothèque DEMONSTRATION :";
        const string ValidEnd = "Fin de la transmission.";
        
        public AbstractInterpreterTest(ITestOutputHelper helper)
        {
            testConsole = new TestConsole(helper);
        }
        
        
        protected virtual void BuildFileInterpreter(string file)
        {
            interpreter= new Interpreter().ForFile(file).WithConsole(testConsole);
        }
        
        protected virtual void BuildSnippetInterpreter(string content)
        {
            string program = $"{ValidHeaderSnippet}\n{ValidStartSnippet}\n{content}{ValidEnd}";
            interpreter = new Interpreter().ForSnippet(program).WithConsole(testConsole);
        }
    }
}