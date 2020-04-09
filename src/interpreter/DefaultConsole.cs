using System;

namespace interpreter
{
    /// <summary>
    /// Redirects to .NET System.Console
    /// </summary>
    public class DefaultConsole : Console
    {
        public void Write(string text)
        {
            System.Console.Write(text);
        }
    }
}