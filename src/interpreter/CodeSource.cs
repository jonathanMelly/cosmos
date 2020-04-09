using System.IO;

namespace interpreter
{
    public class CodeSource
    {
        private string code;
        private string sourceFile;
        
        private CodeSource(string code)
        {
            this.code = code;
        }

        public string Code => code;

        public string SourceFile => sourceFile;

        public static CodeSource FromFile(string file)
        {
            var codeSource = new CodeSource(File.ReadAllText(file));
            codeSource.sourceFile = file;
            return codeSource;
        }

        public static CodeSource FromSnippet(string snippet)
        {
            return new CodeSource(snippet);
        }
    }
}