namespace interpreter
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
            var interpreter = new Interpreter().ForFile(args[0]);
            var result = interpreter.Execute();

            return  (int) (result ? ExitCode.Success : ExitCode.CompileError);

        }
    }
}