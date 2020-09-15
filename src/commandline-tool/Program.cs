using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using lib.interpreter;
using lib.parser;

namespace commandline_tool
{
    /// <summary>
    /// CLI for cosmos
    /// </summary>
    public class Program
    {
        private static string STOP= "stop";
        private const string demo = "Demo.cosmos";
        /// <summary>
        /// App status
        /// </summary>
        public enum ExitCode
        {
            Ok = 0,
            ErreurSyntaxe = 1,
            ErreurExecution =2,
            FichierNonTrouve =3,
            ArgumentInvalide = 4,
            ErreurInconnue = 5

        }

        public static CliOption optInteractive = new CliOption("i", "interactif", "Mode interactif pour tester des commandes");
        public static CliOption optSnippet = new CliOption("c", "code",
            "Permet de passer du code en ligne (au lieu d'un fichier), exemple: --code \"Afficher 5.\"");
        public static CliOption optSyntax = new CliOption("s", "syntaxe", "Vérifie uniquement la syntaxe (pas d'éxécution)",true);
        public static CliOption optDirect = new CliOption("d", "direct", "Stoppe le programme sans devoir appuyer sur Enter",true);

        public static CliOption optHelp = new CliOption("h", "help", "Affiche l'aide");
        public static CliOption optVersion = new CliOption("v", "version", "Affiche la version");

        public static CliOption optNewProgram = new CliOption("n", "nouveau", "Créee un fichier pour un nouveau programme");

        public static CliOption[] options = new CliOption[]
        {
            optInteractive,
            optSnippet,
            optSyntax,
            optDirect,
            optHelp,
            optVersion
        };


        /// <summary>
        /// Interpréteur du langage Cosmos
        /// </summary>
        /// <returns>Un code d'erreur selon ExitCode</returns>
        public static int Main(string[] args)
        {
            Parser parser=null;
            if (args == null || args.Length==0 || args[0].IsMatch(optInteractive))
            {
                //TODO : vérifier qu'aucune autre option n'est passée

                Console.WriteLine($"Mode interactif, essayez 'Afficher \"bonjour\".' par exemple et '{STOP}' pour arrêter.");
                string input="";

                int variablesCount = 0;
                bool goon = true;
                while(goon)
                {
                    Console.Write("=> ");
                    input = Console.ReadLine();

                    //Pour vérifier si une sortie a été effectuée
                    if (input !=null )
                    {
                        if (input.ToLower() != STOP)
                        {
                            Console.Write("Résultat: ");
                            parser = GetParserForEmbeddedSnippet(input, parser);
                            var interpreter = new Interpreter(parser).Execute();

                            //Permet d'afficher un message en cas d'ajout de variable... (TODO: améliorer dans la lib ?)
                            if (parser.Variables.Count != variablesCount)
                            {
                                Console.WriteLine(">Variable enregistrée");
                                variablesCount = parser.Variables.Count;
                            }
                        }
                        else
                        {
                            goon = false;
                        }

                        Console.WriteLine();
                    }

                }

                ResetCursor();
                return (int) ExitCode.Ok;
            }
            else
            {
                //SNIPPET
                if (args[0].IsMatch(optSnippet))
                {
                    var codeContent = new List<string>();
                    foreach (var arg in args[1..])
                    {
                        if(!arg.StartsWith("-"))
                            codeContent.Add(arg);
                    }
                    parser = GetParserForEmbeddedSnippet(String.Join(" ",codeContent.ToArray()));
                }
                //VERSION
                else if (args[0].IsMatch(optVersion))
                {
                    ShowVersion();
                    return (int) ExitCode.Ok;
                }
                //HELP
                else if (args[0].IsMatch(optHelp))
                {
                    ShowHelp();
                    return (int) ExitCode.Ok;
                }
                else if (args[0].IsMatch(optNewProgram))
                {
                    string programName = args.Length>1?args[1]:null;

                    if (programName == null)
                    {
                        Console.Error.WriteLine($"Veuillez indiquer le nom du programme : cosmos {args[0]} <nomDuProgramme>");
                        return (int) ExitCode.ArgumentInvalide;
                    }
                    else
                    {
                        string filename = $"{programName}.cosmos";
                        if (File.Exists(filename))
                        {
                            Console.Error.WriteLine($"Le fichier {filename} existe déjà, veuillez spécifier un nom de programme différent.");
                            return (int) ExitCode.ArgumentInvalide;
                        }
                        else
                        {
                            var content = $"{Parser.BuildValidHeader(programName)}\t//Ajoutez ici vos ordres, par exemple : Afficher \"Bonjour Cosmos\".{Parser.ValidEnd}";
                            File.WriteAllText($"{programName}.cosmos",content);

                            Console.WriteLine($"Nouveau programme généré dans {filename}");
                            Console.WriteLine($"Pour le lancer: cosmos {programName}");
                            return (int) ExitCode.Ok;
                        }
                    }


                }
                //Devrait être un fichier passé en paramètre
                else
                {
                    //Vérifie si pas un argument invalide
                    var remainingArgs = new List<string>(args);

                    //Enlève les options valides
                    foreach (var option in options)
                    {
                        foreach (var optionName in option.Names)
                        {
                            remainingArgs.Remove(optionName);
                        }
                    }

                    //S'il reste un option, elle est invalide
                    var invalidArgs = remainingArgs.Where(s => s.StartsWith("-")).ToList();

                    if (invalidArgs.Count>0)
                    {
                        Console.Error.WriteLine($"Erreur, argument(s) invalide(s): {String.Join(", ",invalidArgs)}.\nPour consulter l'aide, saisissez \"cosmos -h\".");
                        return (int) ExitCode.ArgumentInvalide;
                    }
                    else
                    {
                        if (remainingArgs.Count == 0)
                        {
                            Console.Error.WriteLine($"Erreur, aucun fichier spécifié.");
                            return (int) ExitCode.ArgumentInvalide;
                        }
                        else if(remainingArgs.Count>1)
                        {
                            Console.Error.WriteLine($"Erreur, trop de fichiers spécifiés.");
                            return (int) ExitCode.ArgumentInvalide;
                        }
                        else
                        {
                            var sourceFile = remainingArgs[0];
                            //Prend en compte les fichiers sans spécifier l'extension par défaut de .cosmos
                            if (!File.Exists(sourceFile))
                            {
                                sourceFile = $"{sourceFile}.cosmos";
                            }

                            if (File.Exists(sourceFile))
                            {
                                parser = new Parser().ForFile(sourceFile);
                            }
                            else
                            {
                                Console.Error.WriteLine($"Erreur, le fichier <{sourceFile}> est introuvable.");
                                return (int) ExitCode.FichierNonTrouve;
                            }
                        }
                    }
                }
            }


            if (parser != null)
            {
                var syntaxOnly = optSyntax.IsActive(args);
                var direct = optDirect.IsActive(args);

                var parseResult = parser.Parse();

                if (syntaxOnly)
                {
                    return (int) (parseResult ? ExitCode.Ok : ExitCode.ErreurSyntaxe);
                }
                else
                {
                    var interpreter = new Interpreter(parser);

                    var timer = new Stopwatch();
                    timer.Start();
                    var result = interpreter.Execute();
                    timer.Stop();


                    //Affiche la cartouche uniquement en cas de parsing/éxécution réussie et si pas en mode direct
                    if (!direct && parser.ParsingWasSuccessfull)
                    {
                        var mainMessage = "|Programme cosmos terminé, appuyez sur une touche pour quitter|";
                        var size = mainMessage.Length;
                        var executionTime = $"|Temps d'éxécution: {timer.Elapsed}";
                        executionTime += $"{new String(' ', size - executionTime.Length - 1)}|";

                        var hide = $"|Pour masquer ce message, ajoutez l'option -d ou --direct";
                        hide += $"{new String(' ', size - hide.Length - 1)}|";

                        Console.Write($"{Environment.NewLine}{Environment.NewLine}");
                        Console.WriteLine($"+{new String('-',size-2)}+");
                        Console.WriteLine(mainMessage);
                        Console.WriteLine(executionTime);
                        Console.WriteLine(hide);
                        Console.WriteLine($"+{new String('-',size-2)}+");
                        Console.ReadKey(true);
                    }

                    ResetCursor();

                    return (int) (result ? ExitCode.Ok : !parseResult?ExitCode.ErreurSyntaxe: ExitCode.ErreurExecution);
                }

            }


            //On ne devrait jamais arriver ici
            Console.Error.WriteLine("Erreur inconnue, veuillez consulter l'aide en ligne.");
            return (int)ExitCode.ErreurInconnue;

        }

        /// <summary>
        /// Remet le curseur pour éviter de le cacher dans la console active...
        /// </summary>
        private static void ResetCursor()
        {
            Console.CursorVisible = true;
        }

        private static void ShowVersion()
        {
            Console.WriteLine($"Version: {GetVersion()}");
        }

        private static void ShowHelp()
        {
            string indent = new String(' ', 2);

            ShowVersion();
            Console.WriteLine($"\nUtilisation :\n" +
                              $"{indent}Mode interactif        : cosmos [{optInteractive}]\n" +
                              $"{indent}Mode fichier source    : cosmos <fichierSource> {optSyntax} {optDirect}\n" +
                              $"{indent}Mode extrait de code   : cosmos {{{optSnippet}}} \"Extrait de code source...\" {optSyntax} {optDirect}\n" +
                              $"{indent}Mode nouveau programme : cosmos {{{optNewProgram}}} <nomDuProgramme>");

            Console.WriteLine("\nOptions:");

            foreach (var option in options)
            {
                Console.WriteLine($"{indent}{option.NamesForPrint(" ou ",true),-20} : {option.Description}");
            }

            Console.WriteLine("\nCodes de retour:");

            foreach (int exitCode in Enum.GetValues(typeof (ExitCode)))
            {
                Console.WriteLine($"{indent}{exitCode} : {Enum.GetName(typeof(ExitCode),exitCode)}");
            }
        }

        private static Parser GetParserForEmbeddedSnippet(string snippet,Parser previousContext=null)
        {
            Parser parser;
            parser = new Parser().ForSnippet($"\t\t{snippet}", true);
            if (previousContext != null)
            {
                parser.CopyContext(previousContext);
            }
            return parser;
        }


        public static string GetVersion()
        {
            var version = Assembly.GetEntryAssembly()!
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            return version;
        }

    }

    public class CliOption
    {
        private readonly bool optional;
        private readonly string shortName;
        private readonly string longName;

        public Regex Regex { get; }

        public string Description { get; }


        public CliOption(string shortName, string longName, string description,bool optional=false)
        {
            this.shortName = $"-{shortName}";
            this.longName = $"--{longName}";
            this.optional = optional;
            this.Description = description;

            Regex = new Regex($@"-\b{shortName}\b|--\b{longName}\b");
        }
        public string[] Names => new[]{shortName,longName};

        public string NamesForPrint(string separator="|",bool noOption=false)
        {
            var boxLeft = (optional && !noOption) ? "[" : "";
            var boxRight = (optional && !noOption) ? "]" : "";
            return $"{boxLeft}{shortName} {separator} {longName}{boxRight}";
        }

        public bool IsActive(string[] args)
        {
            return args != null && args.Any(s => s.IsMatch(this));
        }

        public override string ToString()
        {
            return NamesForPrint();
        }
    }



}