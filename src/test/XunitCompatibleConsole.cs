using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using lib.console;
using Xunit.Abstractions;
using Xunit.Sdk;
using static lib.console.IConsole;

namespace test
{
    public class XUnitCompatibleConsole : TextWriter, IConsole
    {
        private readonly IDictionary<Channel, StringBuilder> content =
            new Dictionary<Channel, StringBuilder>
                {[Channel.Standard] = new StringBuilder(),
                    [Channel.Error] = new StringBuilder(),
                    [Channel.Debug] = new StringBuilder()
                };

        private readonly ITestOutputHelper helper;

        public XUnitCompatibleConsole(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        public override Encoding Encoding => Encoding.Default;

        public string Content => content[Channel.Standard].ToString();

        public string DebugContent => content[Channel.Debug].ToString();

        public string ErrorContent => content[Channel.Error].ToString();

        public Stack<string> Input => input;

        private Stack<string> input = new Stack<string>();

        public bool KeyAvailable => input.Count>0;

        string IConsole.ReadKey(bool eatKey)
        {
            return KeyAvailable?input.Pop():null;
        }

        public bool CursorVisible { get; set; }

        public void Write(string text, Channel channel)
        {
            helper.WriteLine(text);
            content[channel].Append(text);
        }

        public void WriteLine(string text, Channel channel)
        {
            Write(text + "\n", channel);
        }

        public string ReadLine()
        {
            return Input.Pop();
        }

        public override string ToString()
        {
            return content.ToString();
        }

        public override void Write(string text)
        {
            Write(text, Channel.Standard);
        }

        public override void WriteLine(string text)
        {
            WriteLine(text, Channel.Standard);
        }

        public void SetCursorToLine(in int index)
        {
            WriteLine($"@@Set cursor y to {index}");
        }

        public void SetCursorToColumn(in int index)
        {
            WriteLine($"@@Set cursor x to {index}");
        }

        public void SetFrontColorTo(string color)
        {
            WriteLine($"@@Set front color to {color}");
        }

        public void SetBackColorTo(string color)
        {
            WriteLine($"@@Set back color to {color}");
        }

        public string WaitForKeyPress(bool eatKey = true)
        {
            var attempt = 0;
            while (input.Count == 0)
            {
                Thread.Sleep(100);

                if (attempt++ > 10)
                {
                    throw new XunitException("No input after 1 second...");
                }
            }

            return input.Pop();
        }

        public void ClearScreen()
        {
            content[Channel.Standard].Clear();
            SetCursorToLine(0);
            SetCursorToColumn(0);
        }

        public int BiggestRow => 0;//not implemented...
    }
}