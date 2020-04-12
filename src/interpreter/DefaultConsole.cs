using System;

namespace interpreter
{
    /// <summary>
    /// Redirects to .NET System.Console
    /// </summary>
    public class DefaultConsole : Console
    {
        public void Write(string text,Console.Channel channel)
        {
            switch (channel)
            {
                case Console.Channel.Standard:
                    System.Console.Write(text);
                    break;
                case Console.Channel.Error:
                    System.Console.Error.Write(text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
            
        }

        public void WriteLine(string text,Console.Channel channel)
        {
            Write($"{text}\n",channel);
        }
    }
}