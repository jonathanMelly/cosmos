using lib.interpreter;
using lib.parser;

namespace commandline_tool
{
    public class Program
    {
        public enum ExitCode
        {
            Success = 0,
            CompileError = 1
        }

        public static int Main(string[] args)
        {
            //TODO gérer les options en ligne de commande (compilation, éxécution, ...)
            var parser = new Parser().ForFile(args[0]);
            var interpreter = new Interpreter(parser);
            var result = interpreter.Execute();

            return (int) (result ? ExitCode.Success : ExitCode.CompileError);
        }
    }
}