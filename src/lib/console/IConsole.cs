namespace lib.console
{
    public interface IConsole
    {
        public enum Channel
        {
            Standard = 1,
            Error = 2,
            Debug = 3
        }

        /// <summary>
        ///     Appends text to the output placeholder
        /// </summary>
        /// <param name="text">to be printed</param>
        /// <param name="channel">1=standard, 2=error</param>
        void Write(string text, Channel channel = Channel.Standard);

        /// <summary>
        ///     Appends text to the output placeholder adding a \n at the end
        /// </summary>
        /// <param name="text">to be printed (\n appended)</param>
        /// <param name="channel">1=standard, 2=error</param>
        void WriteLine(string text, Channel channel = Channel.Standard);

        /// <summary>
        /// Waits and get user input until carriage return
        /// </summary>
        /// <returns>input</returns>
        string ReadLine();
    }
}