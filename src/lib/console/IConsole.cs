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

        /// <summary>
        /// Place the cursor to specified line
        /// </summary>
        /// <param name="index">The zero based line index</param>
        void SetCursorToLine(in int index);

        /// <summary>
        /// Place the cursor to the specified column
        /// </summary>
        /// <param name="index">The zero based column index</param>
        void SetCursorToColumn(in int index);

        /// <summary>
        /// Set front color for following writes
        /// </summary>
        /// <param name="color">the color name</param>
        void SetFrontColorTo(string color);
        /// <summary>
        /// Set back color for following writes
        /// </summary>
        /// <param name="color">the color name</param>
        void SetBackColorTo(string color);
    }
}