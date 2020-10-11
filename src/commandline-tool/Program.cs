using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImGuiNET;
using lib.data;
using lib.interpreter;
using lib.parser;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

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

        private static readonly CliOption OptInteractive = new CliOption("i", "interactif", "Mode interactif pour tester des commandes");
        private static readonly CliOption OptSnippet = new CliOption("c", "code",
            "Permet de passer du code en ligne (au lieu d'un fichier), exemple: --code \"Afficher 5.\"");
        private static readonly CliOption OptSyntax = new CliOption("s", "syntaxe", "Vérifie uniquement la syntaxe (pas d'éxécution)",true);
        private static readonly CliOption OptDirect = new CliOption("d", "direct", "Stoppe le programme sans devoir appuyer sur Enter",true);
        private static readonly CliOption OptRam = new CliOption("r", "ram", "Affiche le contenu de la RAM",true);

        public static readonly CliOption OptHelp = new CliOption("h", "help", "Affiche l'aide");
        private static readonly CliOption OptVersion = new CliOption("v", "version", "Affiche la version");

        private static readonly CliOption OptNewProgram = new CliOption("n", "nouveau", "Créee un fichier pour un nouveau programme");

        private static readonly CliOption[] Options = new CliOption[]
        {
            OptInteractive,
            OptSnippet,
            OptSyntax,
            OptDirect,
            OptRam,
            OptHelp,
            OptVersion
        };

        private static ConsoleColor _defaultForeColor = ConsoleColor.White;
        private static ConsoleColor _defaultBackColor = ConsoleColor.Black;


        //GUI stuff
        private static Sdl2Window _window;
        private static GraphicsDevice _gd=null;
        private static CommandList _cl;
        private static ImGuiController _controller;
        private static readonly Vector3 ClearColor = new Vector3(0.45f, 0.55f, 0.6f);

        private static volatile bool _running = true;
        private static volatile Variables _variables;

        /// <summary>
        /// Interpréteur du langage Cosmos
        /// </summary>
        /// <returns>Un code d'erreur selon ExitCode</returns>
        public static int Main(string[] args)
        {

            try
            {
                _defaultForeColor = Console.ForegroundColor;
                _defaultBackColor = Console.BackgroundColor;
            }
            catch (Exception)
            {
                //
            }

            Parser parser=null;
            if (args == null || args.Length==0 || args[0].IsMatch(OptInteractive))
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

                ResetConsole();
                return (int) ExitCode.Ok;
            }
            else
            {
                //SNIPPET
                if (args[0].IsMatch(OptSnippet))
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
                else if (args[0].IsMatch(OptVersion))
                {
                    ShowVersion();
                    return (int) ExitCode.Ok;
                }
                //HELP
                else if (args[0].IsMatch(OptHelp))
                {
                    ShowHelp();
                    return (int) ExitCode.Ok;
                }
                else if (args[0].IsMatch(OptNewProgram))
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
                    foreach (var option in Options)
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
                            //Prends en compte les fichiers sans spécifier l'extension par défaut de .cosmos
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
                var syntaxOnly = OptSyntax.IsActive(args);
                var direct = OptDirect.IsActive(args);
                var gui = OptRam.IsActive(args);

                var parseResult = parser.Parse();

                if (syntaxOnly)
                {
                    return (int) (parseResult ? ExitCode.Ok : ExitCode.ErreurSyntaxe);
                }
                else
                {
                    var interpreter = new Interpreter(parser);
                    _variables = parser.Variables;

                    var cosmosTask = Task.Factory.StartNew(() =>
                    {
                        var timer = new Stopwatch();
                        timer.Start();
                        var result = interpreter.Execute();
                        timer.Stop();
                        //Affiche la cartouche uniquement en cas de parsing/éxécution réussie et si pas en mode direct
                        if (!direct && parser.ParsingWasSuccessfull)
                        {
                            Console.CursorTop = parser.Console.BiggestRow;
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

                        ResetConsole();
                        _running = false;

                        return (int) (result ? ExitCode.Ok : !parseResult?ExitCode.ErreurSyntaxe: ExitCode.ErreurExecution);
                    });

                    if (gui)
                    {
                        ShowVariablesGui();
                    }

                    return cosmosTask.Result;

                }

            }

            //On ne devrait jamais arriver ici
            Console.Error.WriteLine("Erreur inconnue, veuillez consulter l'aide en ligne.");
            return (int)ExitCode.ErreurInconnue;


        }

        public static void ShowVariablesGui()
        {
            // Create window, GraphicsDevice, and all resources necessary for the demo.
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 350, 200, WindowState.Normal, "Cosmos - RAM"),
                new GraphicsDeviceOptions(true, null, true),
                out _window,
                out _gd);

            _window.Resized += () =>
            {

                _gd.MainSwapchain.Resize((uint) _window.Width, (uint) _window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            ImGui.GetIO().ConfigWindowsResizeFromEdges = false;

            // Main application loop
            while (_window.Exists && _running==true)
            {
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists) { break; }
                _controller.Update(1f / 60f, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

                bool popen=true;

                ImGui.Begin("Contenu de la mémoire RAM",ref popen, ImGuiWindowFlags.None
                        //|ImGuiWindowFlags.NoInputs
                        | ImGuiWindowFlags.NoDecoration
                        | ImGuiWindowFlags.NoMove
                        | ImGuiWindowFlags.NoResize
                        | ImGuiWindowFlags.NoSavedSettings
                        | ImGuiWindowFlags.AlwaysVerticalScrollbar
                        //| ImGuiWindowFlags.NoScrollbar
                        //| ImGuiWindowFlags.NoFocusOnAppearing
                        );
                ImGui.SetWindowSize(new Vector2(_window.Width,_window.Height));
                ImGui.SetWindowPos(new Vector2(0,0));

                foreach (var variable in _variables.Values)
                {
                    ImGui.Text($"{variable.Name}{new String(' ',_variables.LongestName-variable.Name.Length)}: {variable.Value}");
                }

                ImGui.End();

                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(ClearColor.X, ClearColor.Y, ClearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }


            // Clean up Veldrid resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

        /// <summary>
        /// Remet le curseur pour éviter de le cacher dans la console active...
        /// </summary>
        private static void ResetConsole()
        {
            try
            {
                Console.ForegroundColor = _defaultForeColor;
                Console.BackgroundColor = _defaultBackColor;
                Console.CursorVisible = true;

            }
            catch (Exception)
            {
                //It only is a nice to have feature...
            }

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
                              $"{indent}Mode interactif        : cosmos [{OptInteractive}]\n" +
                              $"{indent}Mode fichier source    : cosmos <fichierSource> {OptSyntax} {OptDirect}\n" +
                              $"{indent}Mode extrait de code   : cosmos {{{OptSnippet}}} \"Extrait de code source...\" {OptSyntax} {OptDirect}\n" +
                              $"{indent}Mode nouveau programme : cosmos {{{OptNewProgram}}} <nomDuProgramme>");

            Console.WriteLine("\nOptions:");

            foreach (var option in Options)
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