namespace test.extension
{
    
    public static class StringExtension
    {
        private static string Append(string subject,string toAppend)
        {
            return $"{subject} {toAppend} ";
        }
        
        public static string And(this string subject)
        {
            return Append(subject, Tokens.And);
        }
        
        public static string Or(this string subject)
        {
            return Append(subject, Tokens.Or);
        }
        
        public static string Xor(this string subject)
        {
            return Append(subject, Tokens.Xor);
        }
        
        public static string True(this string subject)
        {
            return Append(subject, Tokens.True);
        }
        
        public static string True2(this string subject)
        {
            return Append(subject, Tokens.True2);
        }
        
        public static string True3(this string subject)
        {
            return Append(subject, Tokens.True3);
        }
        
        public static string False(this string subject)
        {
            return Append(subject, Tokens.False);
        }
        
        public static string False2(this string subject)
        {
            return Append(subject, Tokens.False2);
        }
        
        public static string False3(this string subject)
        {
            return Append(subject, Tokens.False3);
        }


        public static string Group(this string subject, string inside)
        {
            return $"{subject} {Tokens.Lpar}{inside}{Tokens.Rpar}";
        }

    }
    
}