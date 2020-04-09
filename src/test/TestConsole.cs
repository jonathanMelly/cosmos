using System.IO;
using System.Text;
using interpreter;
using Xunit.Abstractions;

namespace test
{
    public class TestConsole : TextWriter, Console
    {
        private readonly StringBuilder content = new StringBuilder();
        private readonly ITestOutputHelper helper;
        public override Encoding Encoding => Encoding.Default;

        public TestConsole(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        public string Content => content.ToString();

        public override void Write(string text)
        {

            helper.WriteLine(text.ToString());
            content.Append(text);
        }
        
        public override void WriteLine(string text)
        {
            Write(text.ToString()+"\n");
        }

        public override string ToString()
        {
            return content.ToString();
        }
    }
}