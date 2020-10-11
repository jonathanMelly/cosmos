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

        private int biggestRow = 0;

        public bool KeyAvailable => Console.KeyAvailable;

        string IConsole.ReadKey(bool eatKey)
        {
            return  KeyAvailable ? Console.ReadKey(eatKey).Key.ToString() : null;
        }

        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }

        public int BiggestRow => biggestRow;

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

            UpdateBiggestRow();
        }

        private void UpdateBiggestRow()
        {
            biggestRow = Math.Max(Console.CursorTop, biggestRow);
        }

        public void WriteLine(string text, IConsole.Channel channel)
        {
            Write($"{text}\n", channel);
            UpdateBiggestRow();
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

            UpdateBiggestRow();

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

        public string WaitForKeyPress(bool eatKey = true)
        {
            return Console.ReadKey(eatKey).Key.ToString();
        }

        public void ClearScreen()
        {
            Console.Clear();
            biggestRow = 0;
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