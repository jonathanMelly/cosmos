using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using lib.antlr;
using lib.console;
using lib.parser.listener;
using lib.parser.type;

namespace lib.parser
{
    public class Parser
    {

        public static string ValidEnd = $"{Environment.NewLine}Fin de la transmission.{Environment.NewLine}";
        public static string ValidHeaderSnippet = BuildValidHeader("TEST_PROG",library:"TEST_LIB");

        private string code;
        private string codeFile;

        private IConsole console;

        private Cosmos.ProgrammeContext context;

        //Keep redirection because we may have more listeners in the future...
        public List<string> Errors => ErrorListener.Errors;

        public IDictionary<string, CosmosVariable> Variables { get; } = new Dictionary<string, CosmosVariable>();

        public ErrorListener ErrorListener { get; private set; }

        public Cosmos.ProgrammeContext Context => context;

        public bool AlreadyParsed { get; private set; }
        public bool ParsingWasSuccessfull { get; private set; }

        public Parser ForFile(string file)
        {
            codeFile = file;
            code = File.ReadAllText(file);
            AlreadyParsed = false;
            return this;
        }

        public Parser ForSnippet(string snippet,bool addHeader = false)
        {
            code = $"{(addHeader?ValidHeaderSnippet:"")}{snippet}{ValidEnd}";
            AlreadyParsed = false;
            return this;
        }

        public Parser WithConsole(IConsole console)
        {
            this.console = console;
            return this;
        }

        public void CopyContext(Parser parser)
        {
            foreach (var existingVariable in parser.Variables)
            {
                Variables.Add(existingVariable);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>true if parsing had no errors</returns>
        public bool Parse()
        {
            if (AlreadyParsed)
            {
                return ParsingWasSuccessfull;
            }

            var antlrInputStream = new AntlrInputStream(code);
            var lexer = new CosmosLexer(antlrInputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new Cosmos(tokens);

            ErrorListener = new ErrorListener(console);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(ErrorListener);

            context = parser.programme();

            var result = !ErrorListener.HadError;

            AlreadyParsed = true;
            ParsingWasSuccessfull = result;

            return result;
        }

        public static string BuildValidHeader(string name,string library=null,string date=null,string newLine =null)
        {
            if (newLine == null)
            {
                newLine = Environment.NewLine;
            }
            return $"Auteur: {Environment.UserName}{newLine}" +
                   $"Date: {date ?? DateTime.Now.ToString("dd.MM.yyyy")}{newLine}" +
                   $"Entreprise: {Environment.UserDomainName}{newLine}" +
                   $"Description: {name}{newLine}{newLine}" +
                   $"Voici les ordres du programme {name} {(library==null?"":$"à classer dans la bibliothèque {library}")} :{newLine}";
        }
    }
}