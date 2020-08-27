using System;

namespace lib.console
{
    /// <summary>
    ///     Redirects to .NET System.Console
    /// </summary>
    public class DefaultConsole : IConsole
    {
        // ReSharper disable once InconsistentNaming
        private const int CONSOLE_WIDTH = 120;

        public void Write(string text, IConsole.Channel channel)
        {
            switch (channel)
            {
                case IConsole.Channel.Standard:
                    Console.Write(text);
                    break;
                case IConsole.Channel.Error:
                    Console.Error.Write(text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }

        public void WriteLine(string text, IConsole.Channel channel)
        {
            Write($"{text}\n", channel);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void SetCursorToLine(in int index)
        {
            if (index > Console.LargestWindowHeight)
            {
                Console.Error.WriteLine($"!ERREUR!>La ligne {index} est en dehors de la console (max={Console.LargestWindowHeight}).\n" +
                                        $"Veuillez modifier les paramètres de la console ou choisir un numéro de ligne inférieur.");
            }
            else
            {
                Console.CursorTop = index;
            }

        }

        public void SetCursorToColumn(in int index)
        {
            if (index > Console.LargestWindowWidth)
            {
                Console.Error.WriteLine($"!ERREUR!>La colonne {index} est en dehors de la console (max={Console.LargestWindowWidth}).\n" +
                                        $"Veuillez modifier les paramètres de la console ou choisir un numéro de colonne inférieur.");
            }
            else
            {
                Console.CursorLeft = index;
            }
        }

        public void SetFrontColorTo(string color)
        {
            Console.ForegroundColor = ExtractColor(color);
        }

        public void SetBackColorTo(string color)
        {
            Console.BackgroundColor = ExtractColor(color);
        }

        private ConsoleColor ExtractColor(string color,bool back=false)
        {
            if (ConsoleColor.TryParse(color, out ConsoleColor result))
            {
                return result;
            }
            else
            {
                return back ? Console.BackgroundColor : Console.ForegroundColor;
            }

        }
    }
}