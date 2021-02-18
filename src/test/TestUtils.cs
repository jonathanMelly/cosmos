using System;
using System.IO;
using System.Text.RegularExpressions;

namespace test
{
    public class TestUtils
    {
        public static string ToDataPath(string filePath=null)
        {
            var absolutePath = Path.GetFullPath(".");

            var dataDirectoryName = "data";
            string dataAbsolutePath;
            
            var regex = new Regex($"(.+src.+{dataDirectoryName}).*");
            if (regex.IsMatch(absolutePath))
            {
                dataAbsolutePath = regex.Replace(absolutePath, "$1");
            }
            else
            {
                dataAbsolutePath = Path.Combine(absolutePath.Substring(0, absolutePath.IndexOf("bin", StringComparison.Ordinal)), dataDirectoryName);
            }

            if (string.IsNullOrEmpty(filePath))
            {
                return $"{dataAbsolutePath}{Path.DirectorySeparatorChar}";
            }
            else
            {
                return Path.Combine(dataAbsolutePath, filePath);
            }

            
        }
    }
}