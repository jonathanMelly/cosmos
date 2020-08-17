namespace commandline_tool
{
    public static class StringExtension
    {
        public static bool IsMatch(this string subject, CliOption option)
        {
            return option.Regex.IsMatch(subject);
        }
    }
}