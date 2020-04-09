namespace interpreter
{
    public interface Console
    {
        /// <summary>
        /// Appends text to the output placeholder
        /// </summary>
        /// <param name="text"></param>
        void Write(string text);
    }
}