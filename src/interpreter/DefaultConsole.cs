using System;

namespace interpreter
{
    /// <summary>
    ///     Redirects to .NET System.Console
    /// </summary>
    public class DefaultConsole : IConsole
    {
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
    }
}